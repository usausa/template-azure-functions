namespace Template;

using Template.Functions.Functions;
using Template.Testing;

public sealed class QueueFunctionTests
{
    [Fact]
    public Task ProcessMessageCompletes()
    {
        var services = HandlerTestHost.CreateServices();

        return QueueFunction.ProcessMessage_Handler("hello", HandlerTestHost.CreateContext(services));
    }
}
