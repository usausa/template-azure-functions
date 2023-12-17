namespace Template.Functions.Functions;

using Microsoft.Azure.WebJobs;

public sealed class TimerFunction
{
    [FunctionName("TimerFunction")]
    public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo timer, ILogger log)
    {
        log.InfoTimerTrigger(DateTime.Now, timer.ScheduleStatus);
    }
}
