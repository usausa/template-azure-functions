namespace Template.Functions.Infrastructure;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

public sealed class ExceptionLoggingMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<ExceptionLoggingMiddleware> log;

    public ExceptionLoggingMiddleware(ILogger<ExceptionLoggingMiddleware> log)
    {
        this.log = log;
    }

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            log.ErrorUnknownException(ex, context.FunctionDefinition.Name);
            throw;
        }
    }
}
