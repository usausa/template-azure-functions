namespace Template.Functions.Functions;

using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Primitives;

using Template.Functions.Infrastructure;
using Template.Functions.Models;
using Template.Models;
using Template.Services;

public sealed class DataFunction
{
    private readonly DataService dataService;

    public DataFunction(DataService dataService)
    {
        this.dataService = dataService;
    }

    //--------------------------------------------------------------------------------
    // Handler
    //--------------------------------------------------------------------------------

    [Function("DataQueryList")]
    public async Task<IResult> QueryList([HttpTrigger(AuthorizationLevel.Function, "get", Route = "data")] HttpRequest request)
    {
        var flag = ParseBool(request.Query["flag"]);
        var limit = ParseInt(request.Query["limit"]) ?? 10;
        var offset = ParseInt(request.Query["offset"]) ?? 0;

        var total = await dataService.CountDataAsync(flag).ConfigureAwait(false);
        var list = await dataService.QueryDataListAsync(flag, limit, offset).ConfigureAwait(false);

        return Results.Ok(new DataListResponse(total, list.Select(MapToResponse).ToList()));
    }

    [Function("DataQuery")]
#pragma warning disable IDE0060
    public async Task<IResult> Query([HttpTrigger(AuthorizationLevel.Function, "get", Route = "data/{id:guid}")] HttpRequest request, Guid id)
#pragma warning restore IDE0060
    {
        var entity = await dataService.QueryDataAsync(id).ConfigureAwait(false);
        return entity is not null ? Results.Ok(MapToResponse(entity)) : Results.NotFound();
    }

    [Function("DataInsert")]
    public async Task<IResult> Insert([HttpTrigger(AuthorizationLevel.Function, "post", Route = "data")] HttpRequest request)
    {
        var (parameter, error) = await RequestValidator.ParseAsync<DataInsertRequest>(request).ConfigureAwait(false);
        if (parameter is null)
        {
            return error!;
        }

        var id = await dataService.InsertDataAsync(parameter.Name, parameter.Flag).ConfigureAwait(false);
        return Results.Created($"/api/data/{id}", new DataInsertResponse(id));
    }

    [Function("DataUpdate")]
    public async Task<IResult> Update([HttpTrigger(AuthorizationLevel.Function, "put", Route = "data/{id:guid}")] HttpRequest request, Guid id)
    {
        var (parameter, error) = await RequestValidator.ParseAsync<DataUpdateRequest>(request).ConfigureAwait(false);
        if (parameter is null)
        {
            return error!;
        }

        var updated = await dataService.UpdateDataAsync(id, parameter.Name, parameter.Flag).ConfigureAwait(false);
        return updated ? Results.NoContent() : Results.NotFound();
    }

    [Function("DataDelete")]
#pragma warning disable IDE0060
    public async Task<IResult> Delete([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "data/{id:guid}")] HttpRequest request, Guid id)
#pragma warning restore IDE0060
    {
        var deleted = await dataService.DeleteDataAsync(id).ConfigureAwait(false);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    //--------------------------------------------------------------------------------
    // Helper
    //--------------------------------------------------------------------------------

    private static DataResponse MapToResponse(DataEntity entity) =>
        new(entity.Id, entity.Name, entity.Flag, entity.UpdateAt);

    private static bool? ParseBool(StringValues values) =>
        Boolean.TryParse(values, out var value) ? value : null;

    private static int? ParseInt(StringValues values) =>
        Int32.TryParse(values, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value) ? value : null;
}
