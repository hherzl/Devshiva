using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WideWorldImporters.API.Models;

public class StockItemsConfiguration : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        // Set configuration for entity
        builder.ToTable("StockItems", "Warehouse");

        // Set key for entity
        builder.HasKey(p => p.StockItemID);

        // Set configuration for columns

        builder.Property(p => p.StockItemName).HasColumnType("nvarchar(200)").IsRequired();
        builder.Property(p => p.SupplierID).HasColumnType("int").IsRequired();
        builder.Property(p => p.ColorID).HasColumnType("int");
        builder.Property(p => p.UnitPackageID).HasColumnType("int").IsRequired();
        builder.Property(p => p.OuterPackageID).HasColumnType("int").IsRequired();
        builder.Property(p => p.Brand).HasColumnType("nvarchar(100)");
        builder.Property(p => p.Size).HasColumnType("nvarchar(40)");
        builder.Property(p => p.LeadTimeDays).HasColumnType("int").IsRequired();
        builder.Property(p => p.QuantityPerOuter).HasColumnType("int").IsRequired();
        builder.Property(p => p.IsChillerStock).HasColumnType("bit").IsRequired();
        builder.Property(p => p.Barcode).HasColumnType("nvarchar(100)");
        builder.Property(p => p.TaxRate).HasColumnType("decimal(18, 3)").IsRequired();
        builder.Property(p => p.UnitPrice).HasColumnType("decimal(18, 2)").IsRequired();
        builder.Property(p => p.RecommendedRetailPrice).HasColumnType("decimal(18, 2)");
        builder.Property(p => p.TypicalWeightPerUnit).HasColumnType("decimal(18, 3)").IsRequired();
        builder.Property(p => p.MarketingComments).HasColumnType("nvarchar(max)");
        builder.Property(p => p.InternalComments).HasColumnType("nvarchar(max)");
        builder.Property(p => p.CustomFields).HasColumnType("nvarchar(max)");
        builder.Property(p => p.LastEditedBy).HasColumnType("int").IsRequired();

        // Columns with default value

        builder
            .Property(p => p.StockItemID)
            .HasColumnType("int")
            .IsRequired()
            .HasDefaultValueSql("NEXT VALUE FOR [Sequences].[StockItemID]");

        // Computed columns

        builder
            .Property(p => p.Tags)
            .HasColumnType("nvarchar(max)")
            .HasComputedColumnSql("json_query([CustomFields],N'$.Tags')");

        builder
            .Property(p => p.SearchDetails)
            .HasColumnType("nvarchar(max)")
            .IsRequired()
            .HasComputedColumnSql("concat([StockItemName],N' ',[MarketingComments])");

        // Columns with generated value on add or update

        builder
            .Property(p => p.ValidFrom)
            .HasColumnType("datetime2")
            .IsRequired()
            .ValueGeneratedOnAddOrUpdate();

        builder
            .Property(p => p.ValidTo)
            .HasColumnType("datetime2")
            .IsRequired()
            .ValueGeneratedOnAddOrUpdate();
    }
}

public class WideWorldImportersDbContext : DbContext
{
    public WideWorldImportersDbContext(DbContextOptions<WideWorldImportersDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations for entity

        modelBuilder
            .ApplyConfiguration(new StockItemsConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<StockItem> StockItems { get; set; }
}

public static class IQueryableExtensions
{
    public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 0, int pageNumber = 0) where TModel : class
        => pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
}

public record SearchStockItemsParameters
{

    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
    public string StockItemName { get; set; }
    public int? SupplierID { get; set; }
}

public static class WideWorldImportersDbContextExtensions
{
    public static IQueryable<StockItem> GetStockItems(this WideWorldImportersDbContext dbContext, SearchStockItemsParameters parameters)
    {
        // Get query from DbSet
        var query = dbContext.StockItems.AsQueryable();

        // Filter by stock item name
        if (!string.IsNullOrEmpty(parameters.StockItemName))
            query = query.Where(item => item.StockItemName.Contains(parameters.StockItemName));

        // Filter by supplier
        if (parameters.SupplierID.HasValue)
            query = query.Where(item => item.SupplierID == parameters.SupplierID);

        return query;
    }

    public static async Task<StockItem> GetStockItemsAsync(this WideWorldImportersDbContext dbContext, StockItem entity)
        => await dbContext.StockItems.SingleOrDefaultAsync(item => item.StockItemID == entity.StockItemID);

    public static async Task<StockItem> GetStockItemsByStockItemNameAsync(this WideWorldImportersDbContext dbContext, StockItem entity)
        => await dbContext.StockItems.FirstOrDefaultAsync(item => item.StockItemName == entity.StockItemName);
}
