using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using Spurious2.Core.LcboImporting.Adapters;
using Spurious2.Core.LcboImporting.Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;

namespace Spurious2.Core.LcboImporting.Services
{
    public class ImportingService : IImportingService
    {
        private readonly IStoreRepository storeRepository;
        private readonly IProductRepository productRepository;
        private readonly IInventoryRepository inventoryRepository;
        private readonly ILcboAdapter lcboAdapter;
        private readonly Regex storeRegex = new Regex("STORE=([0-9]+)");

        public ImportingService(IStoreRepository storeRepository, ILcboAdapter lcboAdapter, IProductRepository productRepository, IInventoryRepository inventoryRepository)
        {
            this.storeRepository = storeRepository;
            this.lcboAdapter = lcboAdapter;
            this.productRepository = productRepository;
            this.inventoryRepository = inventoryRepository;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.storeRepository?.Dispose();
                this.lcboAdapter?.Dispose();
                this.productRepository?.Dispose();
                this.inventoryRepository?.Dispose();
            }
        }

        public async Task<int> ImportStores()
        {
            var storeInfos = await this.lcboAdapter.ReadStores().ConfigureAwait(false);
            await this.storeRepository.Import(storeInfos).ConfigureAwait(false);
            return storeInfos.Count();
        }

        public async Task<int> ImportProducts()
        {
            var products = new ConcurrentBag<Product>();
            var productIds = await this.lcboAdapter.ReadProductIds().ConfigureAwait(false);
            var productIdsToQuery = productIds;

            Parallel.ForEach(productIdsToQuery, productId =>
            {
                var product = this.lcboAdapter.ReadProductS(productId);
                products.Add(product);
            });

            await this.productRepository.Import(products.Where(p => p.LiquorType.ToUpperInvariant() == "BEER"
            || p.LiquorType.ToUpperInvariant() == "WINE"
            || p.LiquorType.ToUpperInvariant() == "SPIRITS"))
                .ConfigureAwait(false);
            return products.Count;
        }

        public async Task<int> ReadInventoryHtmlsIntoDatabase()
        {
            var productIds = await this.productRepository.GetProductIds().ConfigureAwait(false);
            await this.inventoryRepository.ClearInventoryPages().ConfigureAwait(false);
            const int skipInterval = 6;
            var skip = 0;
            var getAllStopwatch = Stopwatch.StartNew();
            while (skip < productIds.Count())
            {
                var toGet = productIds.Skip(skip).Take(skipInterval).ToList();
                var htmlTasks = new Task<string>[toGet.Count];

                for (var i = 0; i < toGet.Count; i++)
                {
                    htmlTasks[i] = this.lcboAdapter.ReadInventoryHtmlAsync(toGet[i]);
                }

                Task.WaitAll(htmlTasks);

                var dbImportTasks = new Task[toGet.Count];
                for (var i = 0; i < toGet.Count; i++)
                {
                    dbImportTasks[i] = this.inventoryRepository.ImportHtml(htmlTasks[i].Result, toGet[i]);
                }

                Task.WaitAll(dbImportTasks);

                skip += skipInterval;
            }

            getAllStopwatch.Stop();
            Console.WriteLine($"Took {getAllStopwatch.Elapsed} to get all htmls");
            return productIds.Count();
        }

        public async Task<int> UpdateInventoriesFromDatabase(int skip = 0, int take = 100000)
        {
            var readAllStopwatch = Stopwatch.StartNew();
            var productsStopwatch = Stopwatch.StartNew();
            var productIds = await this.productRepository.GetProductIds().ConfigureAwait(false);
            productsStopwatch.Stop();
            Console.WriteLine($"Took {productsStopwatch.Elapsed} to get all product IDs");
            const int skipInterval = 2;
            var skipForThreads = 0;
            var inventories = new List<Inventory>(); //await this.inventoryRepository.GetInventories().ConfigureAwait(false);
            //var inventoryDict = inventories.ToDictionary(i => $"{i.ProductId}_{i.StoreId}", i => i);
            productIds = productIds.Skip(skip).Take(take);
            if (skip == 0)
            {
                await this.inventoryRepository.ClearIncomingInventory().ConfigureAwait(false);
            }

            while (skipForThreads < productIds.Count())
            {
                var productIdsToGet = productIds.Skip(skipForThreads).Take(skipInterval).ToList();
                var readInventoryTasks = new Task<List<Inventory>>[productIdsToGet.Count];
                var parsestopwatch = Stopwatch.StartNew();
                for (var i = 0; i < productIdsToGet.Count; i++)
                {
                    readInventoryTasks[i] = this.ReadInventoriesFromDatabase(productIdsToGet[i]);
                }

                Task.WaitAll(readInventoryTasks);
                parsestopwatch.Stop();
                //Console.WriteLine($"Took {parsestopwatch.Elapsed} to parse {productIdsToGet.Count} product inventories");
                for (var i = 0; i < productIdsToGet.Count; i++)
                {
                    inventories.AddRange(readInventoryTasks[i].Result);
                    //Console.WriteLine($"There are now {inventories.Count} inventories");
                }

                skipForThreads += skipInterval;
            }

            var writestopwatch = Stopwatch.StartNew();
            await this.inventoryRepository.Import(inventories).ConfigureAwait(false);
            writestopwatch.Stop();
            Console.WriteLine($"Took {writestopwatch.Elapsed} to import {inventories.Count} inventories");
            if (productIds.Count() < take)
            {
                var updatestopwatch = Stopwatch.StartNew();
                await this.inventoryRepository.UpdateInventoriesFromIncoming().ConfigureAwait(false);
                updatestopwatch.Stop();
                Console.WriteLine($"Took {updatestopwatch.Elapsed} to update inventories from incoming");
            }

            readAllStopwatch.Stop();
            Console.WriteLine($"Took {readAllStopwatch.Elapsed} to read all htmls");
            return productIds.Count();
        }

        private async Task<List<Inventory>> ReadInventoriesFromDatabase(int productId)
        {
            var html = await this.inventoryRepository.GetHtmlForIdAsync(productId).ConfigureAwait(false);
            // Parse html
            var collection = new List<Inventory>(); // parse html
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var trNodes = doc.DocumentNode.QuerySelectorAll("form[name=\"inventoryresults\"] table[border=\"0\"][width=\"100%\"][cellpadding=\"5\"] tr");
            foreach (var tr in trNodes)
            {
                var tdPNodes = tr.QuerySelectorAll("td p");
                if (tdPNodes.Count() == 4)
                {
                    var storeLinkAttribute = tdPNodes.ElementAt(1).QuerySelector("a").Attributes["href"];
                    var r = storeRegex.Match(storeLinkAttribute.Value);
                    var storeId = Convert.ToInt32(r.Groups[1].Value, CultureInfo.InvariantCulture);
                    var quantity = Convert.ToInt32(tdPNodes.ElementAt(3).InnerText, CultureInfo.InvariantCulture);
                    collection.Add(new Inventory { ProductId = productId, Quantity = quantity, StoreId = storeId });
                }
            }

            return collection;
        }
    }
}
