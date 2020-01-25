using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Spurious2.Core.Reading.Domain;

namespace Spurious2.Repositories.Reading.Contexts
{
    public partial class SpuriousContext : DbContext
    {
        public SpuriousContext()
        {
        }

        public SpuriousContext(DbContextOptions<SpuriousContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        //public virtual DbSet<Subdivision> Subdivisions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile("appsettings.json", optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("SpuriousDb");
                optionsBuilder.UseNpgsql(connectionString, x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack")
                .HasPostgresExtension("postgis")
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            //modelBuilder.Entity<Inventory>(entity =>
            //{
            //    entity.HasKey(e => new { e.ProductId, e.StoreId })
            //        .HasName("inventories_pkey");

            //    entity.ToTable("inventories");

            //    entity.HasIndex(e => e.StoreId)
            //        .HasName("fki_inventories_stores_id_fkey");

            //    entity.Property(e => e.ProductId).HasColumnName("product_id");

            //    entity.Property(e => e.StoreId).HasColumnName("store_id");

            //    entity.Property(e => e.Quantity).HasColumnName("quantity");

            //    entity.HasOne(d => d.Product)
            //        .WithMany(p => p.Inventories)
            //        .HasForeignKey(d => d.ProductId)
            //        .HasConstraintName("inventories_product_id_fkey");

            //    entity.HasOne(d => d.Store)
            //        .WithMany(p => p.Inventories)
            //        .HasForeignKey(d => d.StoreId)
            //        .HasConstraintName("inventories_store_id_fkey");
            //});

            //modelBuilder.Entity<Product>(entity =>
            //{
            //    entity.ToTable("products");

            //    entity.Property(e => e.Id)
            //        .HasColumnName("id")
            //        .ValueGeneratedNever();

            //    entity.Property(e => e.Category).HasColumnName("category");

            //    entity.Property(e => e.Name).HasColumnName("name");

            //    entity.Property(e => e.Volume).HasColumnName("volume");
            //});

            //modelBuilder.Entity<Store>(entity =>
            //{
            //    entity.ToTable("stores");

            //    entity.HasIndex(e => e.Location)
            //        .HasName("idx_location")
            //        .ForNpgsqlHasMethod("gist");

            //    entity.Property(e => e.Id)
            //        .HasColumnName("id")
            //        .ValueGeneratedNever();

            //    entity.Property(e => e.BeerVolume).HasColumnName("beer_volume");

            //    entity.Property(e => e.City).HasColumnName("city");

            //    entity.Property(e => e.Latitude)
            //        .HasColumnName("latitude")
            //        .HasColumnType("numeric(7,4)");

            //    entity.Property(e => e.Location)
            //        .HasColumnName("location")
            //        .HasColumnType("geography");

            //    entity.Property(e => e.Longitude)
            //        .HasColumnName("longitude")
            //        .HasColumnType("numeric(7,4)");

            //    entity.Property(e => e.Name).HasColumnName("name");

            //    entity.Property(e => e.SpiritsVolume).HasColumnName("spirits_volume");

            //    entity.Property(e => e.WineVolume).HasColumnName("wine_volume");
            //});

            //modelBuilder.Entity<Subdivision>(entity =>
            //{
            //    entity.ToTable("subdivisions");

            //    entity.HasIndex(e => e.Boundry)
            //        .HasName("idx_boundary")
            //        .ForNpgsqlHasMethod("gist");

            //    entity.Property(e => e.Id)
            //        .HasColumnName("id")
            //        .ValueGeneratedNever();

            //    entity.Property(e => e.AverageAfterTaxIncome).HasColumnName("average_after_tax_income");

            //    entity.Property(e => e.AverageIncome).HasColumnName("average_income");

            //    entity.Property(e => e.BeerVolume).HasColumnName("beer_volume");

            //    entity.Property(e => e.BoundaryGml).HasColumnName("boundary_gml");

            //    entity.Property(e => e.Boundry)
            //        .HasColumnName("boundry")
            //        .HasColumnType("geography");

            //    entity.Property(e => e.MedianAfterTaxIncome).HasColumnName("median_after_tax_income");

            //    entity.Property(e => e.MedianIncome).HasColumnName("median_income");

            //    entity.Property(e => e.Name).HasColumnName("name");

            //    entity.Property(e => e.Population).HasColumnName("population");

            //    entity.Property(e => e.Province).HasColumnName("province");

            //    entity.Property(e => e.SpiritsVolume).HasColumnName("spirits_volume");

            //    entity.Property(e => e.WineVolume).HasColumnName("wine_volume");
            //});
        }
    }
}
