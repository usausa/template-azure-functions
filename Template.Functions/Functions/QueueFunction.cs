namespace Template.Functions.Functions;

using AzureFunctionsExtension.Annotations;

[AzureFunction]
public sealed partial class QueueFunction
{
    private readonly ILogger<QueueFunction> log;

    public QueueFunction(ILogger<QueueFunction> log)
    {
        this.log = log;
    }

    [QueueEndpoint("template-queue")]
    public void ProcessMessage([FromTrigger] string message)
    {
        log.InfoQueueTrigger(message);
    }
}
