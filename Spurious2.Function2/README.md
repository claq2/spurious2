# How It Works

StartImporting

- storeRepository.ClearIncomingStores().ConfigAwait();
- productRepository.ClearIncomingProducts().ConfigAwait();
- inventoryRepository.ClearIncomingInventory().ConfigAwait();
- storageAdapter.ClearStorage();
  - var productsClient = clientFactory.Invoke("products");
  - var inventoriesClient = clientFactory.Invoke("inventories");
  - var storesClient = clientFactory.Invoke("stores");
  - var lastProductClient = clientFactory.Invoke("last-product");
  - var lastInventoryClient = clientFactory.Invoke("last-inventory");

GetWinePages/Beer/Spirits in parallel

- foreach product subtype e.g. Wine.Red, Wine.White, Wine.Champagne
  - read products 1 page of 9 at a time. Return a page of parsed products.
    - put page of products into ProductsIncoming
    - write each product ID to storage products/{productId}

SignalLastProductDone

- writes a message to last-product storage

Product ID written to storage BlobTrigger("products/{productId}")

- get all stores inventory for product
- write contents to inventories/{productId}

Inventory written to storage BlobTrigger("inventories/{name}")

- extract list of inventory at each store and the store URLs
- write store IDs to IncomingStores
- write inventories to IncomingInventories
- if store ID doesn't exist in storage, visit the URL and write contents to store storage
- mark IncomingProduct done

Store ID written to storage BlobTrigger("stores/{storeId}")

- read store info from storage
- extract store info
- write store info to IncomingStores, which only has its ID right now

Message written to last-product storage BlobTrigger("last-product/{name}")

- writes a message to last-inventory

Message written to last-inventory storage BlobTrigger("last-inventory/{name}")

- calls EndImporting, which does nothing.
