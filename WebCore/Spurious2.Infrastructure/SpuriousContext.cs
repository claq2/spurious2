using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Spurious2.Core2.Inventories;
using Spurious2.Core2.Products;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Infrastructure;

public partial class SpuriousContext : DbContext
{
    public SpuriousContext()
    {
    }

    public SpuriousContext(DbContextOptions<SpuriousContext> options)
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

    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BoundaryIncomingConfiguration());

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
            entity.Property(e => e.SubdivisionName).HasColumnType("nvarchar(255)");
            entity.Property(e => e.Province).HasColumnType("nvarchar(255)").IsRequired();
            entity.Property(e => e.Population).HasColumnType("int").IsRequired();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);//.HasName("PK__Product__3214EC07712F54DE");

            entity.ToTable("Product");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasColumnType("nvarchar(255)");
        });

        modelBuilder.Entity<ProductIncoming>(entity =>
        {
            entity.HasKey(e => e.Id);//.HasName("pri_firstkey");

            entity.ToTable("ProductIncoming");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.ProductDone).HasDefaultValue(false);
            entity.Ignore(e => e.ProductPageUrl);
            entity.Ignore(e => e.Size);
        });

        modelBuilder.ApplyConfiguration(new StoreConfiguration());

        modelBuilder.Entity<StoreIncoming>(entity =>
        {
            entity.ToTable("StoreIncoming");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(255);
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.StoreName).HasMaxLength(255);
            entity.Property(e => e.LocationGeog).HasColumnName("Location")
                .HasColumnType("geography");
            entity.Property(e => e.StoreDone).HasDefaultValue(false);
        });

        modelBuilder.Entity<Subdivision>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Subdivision");

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
