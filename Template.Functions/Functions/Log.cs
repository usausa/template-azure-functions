namespace Template.Functions.Functions;

using Microsoft.Azure.WebJobs.Extensions.Timers;

#pragma warning disable SYSLIB1006
public static partial class Log
{
    [LoggerMessage(Level = LogLevel.Information, Message = "C# HTTP trigger function processed a request.")]
    public static partial void InfoHttpTrigger(this ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "C# Timer trigger function executed at: {dateTime} {status}")]
    public static partial void InfoTimerTrigger(this ILogger logger, DateTime dateTime, ScheduleStatus status);
}
#pragma warning restore SYSLIB1006
