namespace Template.Functions.Functions;

using AzureFunctionsExtension;
using AzureFunctionsExtension.Annotations;

using Microsoft.Azure.Functions.Worker;

using Template.Services;

using IActionResult = Microsoft.AspNetCore.Mvc.IActionResult;

[AzureFunction]
public sealed partial class HttpFunction
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

    [HttpEndpoint("get", "hello", AuthorizationLevel.Anonymous)]
    public IActionResult Hello([FromQuery] string? name)
    {
        log.InfoHttpTrigger();

        if (String.IsNullOrEmpty(name))
        {
            return Results.BadRequest();
        }

        return Results.Ok($"{service.GetTimestamp()} : Hello, {name}.");
    }
}
