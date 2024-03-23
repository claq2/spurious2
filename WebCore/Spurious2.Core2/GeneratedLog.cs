using Microsoft.Extensions.Logging;

namespace Spurious2.Core2;

public static partial class GeneratedLog
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Error, Message = "An error occurred while migrating the database.")]
    public static partial void MigrationError(this ILogger logger, Exception ex);

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Deleting containers")]
    public static partial void DeletingContainers(this ILogger logger);

    [LoggerMessage(EventId = 2, Level = LogLevel.Information, Message = "Deleted containers")]
    public static partial void DeletedContainers(this ILogger logger);

    [LoggerMessage(EventId = 3, Level = LogLevel.Information, Message = "Creating containers attempt {Attempt}")]
    public static partial void CreatingContainers(this ILogger logger, int attempt);

    [LoggerMessage(EventId = 4, Level = LogLevel.Information, Message = "Created products")]
    public static partial void CreatedProducts(this ILogger logger);

    [LoggerMessage(EventId = 5, Level = LogLevel.Warning, Message = "Couldn't create products")]
    public static partial void CouldNotCreateProducts(this ILogger logger, Exception ex);

    [LoggerMessage(EventId = 6, Level = LogLevel.Information, Message = "Created inventories")]
    public static partial void CreatedInventories(this ILogger logger);

    [LoggerMessage(EventId = 7, Level = LogLevel.Warning, Message = "Couldn't create inventories")]
    public static partial void CouldNotCreateInventories(this ILogger logger, Exception ex);

    [LoggerMessage(EventId = 8, Level = LogLevel.Information, Message = "Created stores")]
    public static partial void CreatedStores(this ILogger logger);

    [LoggerMessage(EventId = 9, Level = LogLevel.Warning, Message = "Couldn't create stores")]
    public static partial void CouldNotCreateStores(this ILogger logger, Exception ex);

    [LoggerMessage(EventId = 10, Level = LogLevel.Information, Message = "Created last-product")]
    public static partial void CreatedLastProduct(this ILogger logger);

    [LoggerMessage(EventId = 11, Level = LogLevel.Warning, Message = "Couldn't create last-product")]
    public static partial void CouldNotCreateLastProduct(this ILogger logger, Exception ex);

    [LoggerMessage(EventId = 12, Level = LogLevel.Information, Message = "Created last-inventory")]
    public static partial void CreatedLastInventory(this ILogger logger);

    [LoggerMessage(EventId = 13, Level = LogLevel.Warning, Message = "Couldn't create last-inventory")]
    public static partial void CouldNotCreateLastInventory(this ILogger logger, Exception ex);

    [LoggerMessage(EventId = 14, Level = LogLevel.Error, Message = "Failed to create one of the containers after 3 tries")]
    public static partial void FailedToCreateContainer(this ILogger logger);

    [LoggerMessage(EventId = 15, Level = LogLevel.Information, Message = "Cleared for importing")]
    public static partial void ClearedForImporting(this ILogger logger);

    [LoggerMessage(EventId = 16, Level = LogLevel.Information, Message = "Last product done")]
    public static partial void SignalLastProductDone(this ILogger logger);

    [LoggerMessage(EventId = 17, Level = LogLevel.Information, Message = "Processed product {ProductId}")]
    public static partial void ProcessedProduct(this ILogger logger, string productId);

    [LoggerMessage(EventId = 18, Level = LogLevel.Information, Message = "Found {Count} inventory items for product {ProductId}")]
    public static partial void FoundInventoryForProduct(this ILogger logger, int count, string productId);

    [LoggerMessage(EventId = 19, Level = LogLevel.Information, Message = "Processed inventory {ProductId}")]
    public static partial void ProcessedInventory(this ILogger logger, string productId);

    [LoggerMessage(EventId = 20, Level = LogLevel.Information, Message = "Processed store {StoreId}")]
    public static partial void ProcessedStore(this ILogger logger, string storeId);

    [LoggerMessage(EventId = 21, Level = LogLevel.Information, Message = "Processed last product {Contents}")]
    public static partial void ProcessedLastProduct(this ILogger logger, string contents);

    [LoggerMessage(EventId = 22, Level = LogLevel.Information, Message = "Processed last inventory {Contents}")]
    public static partial void ProcessedLastInventory(this ILogger logger, string contents);

    [LoggerMessage(EventId = 23, Level = LogLevel.Information, Message = "Ended importing, doing DB update (no, not really :)")]
    public static partial void EndedImporting(this ILogger logger);

    [LoggerMessage(EventId = 24, Level = LogLevel.Information, Message = "Ended DB update")]
    public static partial void EndedDbUpdate(this ILogger logger);
}
