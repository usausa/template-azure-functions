namespace Template.Functions.Functions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

using Template.Functions.Infrastructure;
using Template.Services;

[ExceptionLoggingFilter]
public class HttpFunction
{
    private readonly ILogger<HttpFunction> log;

    private readonly Service service;

    public HttpFunction(
        ILogger<HttpFunction> log,
        Service service)
    {
        this.log = log;
        this.service = service;
    }

    [FunctionName("HttpFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest request)
    {
        log.InfoHttpTrigger();

        var name = (string)request.Query["name"];
        if (String.IsNullOrEmpty(name))
        {
            return new BadRequestResult();
        }

        return new OkObjectResult($"{service.GetTimestamp()} : Hello, {name}.");
    }
}
