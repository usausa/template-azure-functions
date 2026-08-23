namespace Template.Functions.Functions;

using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;

using Template.Services;

public sealed class HttpFunction
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

    [Function("HttpFunction")]
    public IResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest request)
    {
        log.InfoHttpTrigger();

        var name = (string?)request.Query["name"];
        if (String.IsNullOrEmpty(name))
        {
            return Results.BadRequest();
        }

        return Results.Ok($"{service.GetTimestamp()} : Hello, {name}.");
    }
}
