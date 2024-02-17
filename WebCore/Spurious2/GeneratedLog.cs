namespace Spurious2;

public static partial class GeneratedLog
{
    [LoggerMessage(EventId = 0, Level = LogLevel.Error, Message = "An error occurred while migrating the database.")]
    public static partial void MigrationError(this ILogger logger, Exception ex);
}
