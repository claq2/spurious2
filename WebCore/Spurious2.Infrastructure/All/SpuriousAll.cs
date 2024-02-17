using Microsoft.EntityFrameworkCore;
using Spurious2.Core.Boundaries;
using Spurious2.Core.Inventories;
using Spurious2.Core.Populations;
using Spurious2.Core.Products;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Infrastructure.All;

public partial class SpuriousAll : DbContext
{
    public SpuriousAll()
    {
    }

    public SpuriousAll(DbContextOptions<SpuriousAll> options)
        : base(options)
    {
    }

    public virtual DbSet<BoundaryIncoming> BoundaryIncomings { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryIncoming> InventoryIncomings { get; set; }

    public virtual DbSet<PopulationIncoming> PopulationIncomings { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductIncoming> ProductIncomings { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreIncoming> StoreIncomings { get; set; }

    public virtual DbSet<Subdivision> Subdivisions { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoundaryIncoming>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("BoundaryIncoming");

            entity.Property(e => e.Province).HasMaxLength(255);
            entity.Property(e => e.SubdivisionName).HasMaxLength(255);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.StoreId });

            entity.ToTable("Inventory");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Inventory_Product");

            entity.HasOne(d => d.Store).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Inventory_Store");
        });

        modelBuilder.Entity<InventoryIncoming>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.StoreId });

            entity.ToTable("InventoryIncoming");
        });

        modelBuilder.Entity<PopulationIncoming>(entity =>
        {
            entity.HasKey(e => e.Id);//.HasName("pi_firstkey");

            entity.ToTable("PopulationIncoming");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SubdivisionName).HasColumnType("text");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);//.HasName("PK__Product__3214EC07712F54DE");

            entity.ToTable("Product");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasColumnType("text");
        });

        modelBuilder.Entity<ProductIncoming>(entity =>
        {
            entity.HasKey(e => e.Id);//.HasName("pri_firstkey");

            entity.ToTable("ProductIncoming");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.ProductDone).HasDefaultValue(false);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id);//.HasName("stores_pkey");

            entity.ToTable("Store");

            // entity.HasIndex(e => e.Location, "SPATIAL_Store");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City).HasColumnType("text");
            entity.Property(e => e.StoreName).HasColumnType("text");
            entity.Property(e => e.LocationGeog).HasColumnName("Location")
                .HasColumnType("geography");
            entity.Ignore(e => e.Location);
        });

        modelBuilder.Entity<StoreIncoming>(entity =>
        {
            entity.ToTable("StoreIncoming");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.StoreName).HasMaxLength(255);
        });

        modelBuilder.Entity<Subdivision>(entity =>
        {
            entity.HasKey(e => e.Id);//.HasName("firstkey");

            entity.ToTable("Subdivision");

            // entity.HasIndex(e => e.Boundary, "SPATIAL_Subdivision");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AlcoholDensity).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.BeerDensity).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.Province).HasMaxLength(255);
            entity.Property(e => e.SpiritsDensity).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.SubdivisionName).HasMaxLength(255);
            entity.Property(e => e.WineDensity).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.GeographicCentreGeog).HasColumnName("GeographicCentre")
                .HasColumnType("geography");
            entity.Ignore(e => e.GeographicCentre);
            entity.Ignore(e => e.RequestedDensityAmount);
            entity.Property(e => e.Population).HasDefaultValue(0);
            entity.Property(e => e.BeerVolume).HasDefaultValue(0);
            entity.Property(e => e.WineVolume).HasDefaultValue(0);
            entity.Property(e => e.SpiritsVolume).HasDefaultValue(0);
            entity.Property(e => e.AverageIncome).HasDefaultValue(0);
            entity.Property(e => e.MedianIncome).HasDefaultValue(0);
            entity.Property(e => e.MedianAfterTaxIncome).HasDefaultValue(0);
            entity.Property(e => e.AverageAfterTaxIncome).HasDefaultValue(0);
        });

        this.OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
