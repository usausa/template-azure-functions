namespace Template.Functions.Functions;

using AzureFunctionsExtension.Annotations;

using Microsoft.Azure.Functions.Worker;

[AzureFunction]
public sealed partial class TimerFunction
{
    private readonly ILogger<TimerFunction> log;

    private readonly TimeProvider timeProvider;

    public TimerFunction(
        ILogger<TimerFunction> log,
        TimeProvider timeProvider)
    {
        this.log = log;
        this.timeProvider = timeProvider;
    }

    [TimerEndpoint("0 */5 * * * *")]
    public void ProcessTimer([FromTrigger] TimerInfo timer)
    {
        var now = timeProvider.GetLocalNow().DateTime;
        log.InfoTimerTrigger(now, timer.ScheduleStatus);
    }
}
