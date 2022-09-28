// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Logging;

// ReSharper disable once InconsistentNaming
public static class ILoggerExtensions
{
    public static void LogException(this ILogger logger, Exception ex)
    {
        logger.LogError("The exception was thrown: {ex}", ex);
    }
}