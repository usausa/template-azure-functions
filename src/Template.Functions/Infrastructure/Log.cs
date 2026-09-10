namespace Template.Functions.Infrastructure;

internal static partial class Log
{
    [LoggerMessage(Level = LogLevel.Error, Message = "Unhandled exception. function=[{function}]")]
    public static partial void ErrorUnknownException(this ILogger logger, Exception ex, string function);
}
