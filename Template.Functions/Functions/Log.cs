namespace Template.Functions.Functions;

using Microsoft.Azure.Functions.Worker;

internal static partial class Log
{
    [LoggerMessage(Level = LogLevel.Information, Message = "C# HTTP trigger function processed a request.")]
    public static partial void InfoHttpTrigger(this ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "C# Timer trigger function executed at: {dateTime} {status}")]
    public static partial void InfoTimerTrigger(this ILogger logger, DateTime dateTime, ScheduleStatus? status);

    [LoggerMessage(Level = LogLevel.Information, Message = "C# Queue trigger function processed a message. message=[{message}]")]
    public static partial void InfoQueueTrigger(this ILogger logger, string message);
}
