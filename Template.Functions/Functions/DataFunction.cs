namespace Template.Functions.Functions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

using Template.Functions.Infrastructure;
using Template.Services;

public class DataInsertRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public bool Flag { get; set; }
}

public class DataUpdateRequest
{
    public string Name { get; set; } = default!;

    public bool Flag { get; set; }
}

[ExceptionLoggingFilter]
public class DataFunction
{
    private readonly DataService dataService;

    public DataFunction(DataService dataService)
    {
        this.dataService = dataService;
    }

    [FunctionName("DataQueryList")]
    public async Task<IActionResult> QueryList([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "data")] HttpRequest request)
    {
        var list = await dataService.QueryDataListAsync(null, 10, 0).ConfigureAwait(false);

        // TODO
        return new OkObjectResult(list.ToArray());
    }

    //[FunctionName("DataQuery")]
    //public IActionResult Query([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "data/{id}")] HttpRequest request, Guid id)
    //{
    //    return new OkResult();
    //}

    //[FunctionName("DataInsert")]
    //public IActionResult Insert([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "data")] HttpRequest request)
    //{
    //    return new OkResult();
    //}

    //[FunctionName("DataUpdate")]
    //public IActionResult Update([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "data/{id}")] HttpRequest request, Guid id)
    //{
    //    return new OkResult();
    //}

    //[FunctionName("DataDelete")]
    //public IActionResult Delete([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "data/{id}")] HttpRequest request, Guid id)
    //{
    //    return new OkResult();
    //}
}
