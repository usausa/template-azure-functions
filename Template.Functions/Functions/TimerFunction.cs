namespace Template.Functions.Functions;

using Microsoft.Azure.Functions.Worker;

public sealed class TimerFunction
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

    [Function("TimerFunction")]
    public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo timer)
    {
        var now = timeProvider.GetLocalNow().DateTime;
        log.InfoTimerTrigger(now, timer.ScheduleStatus);
    }
}
