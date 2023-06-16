namespace Template.Functions.Infrastructure;

using Microsoft.Azure.WebJobs.Host;

#pragma warning disable CS0618
public sealed class ExceptionLoggingFilter : FunctionExceptionFilterAttribute
{
    public override Task OnExceptionAsync(FunctionExceptionContext exceptionContext, CancellationToken cancellationToken)
    {
        exceptionContext.Logger.ErrorUnknownException(exceptionContext.Exception, exceptionContext.FunctionName);
        return Task.CompletedTask;
    }
}
#pragma warning restore CS0618
