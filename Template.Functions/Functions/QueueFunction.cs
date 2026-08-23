namespace Template.Functions.Functions;

using Microsoft.Azure.Functions.Worker;

public sealed class QueueFunction
{
    private readonly ILogger<QueueFunction> log;

    public QueueFunction(ILogger<QueueFunction> log)
    {
        this.log = log;
    }

    [Function("QueueFunction")]
    public void Run([QueueTrigger("template-queue")] string message)
    {
        log.InfoQueueTrigger(message);
    }
}
