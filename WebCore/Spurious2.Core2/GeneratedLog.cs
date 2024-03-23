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
}
