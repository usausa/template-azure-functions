namespace Template.Functions.Functions;

using AzureFunctionsExtension;
using AzureFunctionsExtension.Annotations;

using Smart.Mapper;

using Template.Functions.Models;
using Template.Models;
using Template.Services;

using IActionResult = Microsoft.AspNetCore.Mvc.IActionResult;

[AzureFunction]
public sealed partial class DataFunction
{
    private const int MaxLimit = 200;

    private readonly DataService dataService;

    public DataFunction(DataService dataService)
    {
        this.dataService = dataService;
    }

    //--------------------------------------------------------------------------------
    // Handler
    //--------------------------------------------------------------------------------

    [Mapper]
    private static partial DataResponse ToResponse(DataEntity entity);

    [HttpEndpoint("get", "data")]
    public async Task<IActionResult> DataQueryList(
        [FromQuery] bool? flag,
        [FromQuery] int limit = 10,
        [FromQuery] int offset = 0)
    {
        if (limit is < 1 or > MaxLimit)
        {
            return Results.BadRequest("Invalid parameter: limit");
        }

        if (offset < 0)
        {
            return Results.BadRequest("Invalid parameter: offset");
        }

        var total = await dataService.CountDataAsync(flag).ConfigureAwait(false);
        var list = await dataService.QueryDataListAsync(flag, limit, offset).ConfigureAwait(false);

        return Results.Ok(new DataListResponse(total, list.Select(ToResponse).ToList()));
    }

    [HttpEndpoint("get", "data/{id:guid}")]
    public async Task<IActionResult> DataQuery([FromRoute] Guid id)
    {
        var entity = await dataService.QueryDataAsync(id).ConfigureAwait(false);
        return entity is not null ? Results.Ok(ToResponse(entity)) : Results.NotFound();
    }

    [HttpEndpoint("post", "data")]
    public async Task<IActionResult> DataInsert([FromBody] DataInsertRequest request)
    {
        var id = await dataService.InsertDataAsync(request.Name, request.Flag).ConfigureAwait(false);
        return Results.Created($"/api/data/{id}", new DataInsertResponse(id));
    }

    [HttpEndpoint("put", "data/{id:guid}")]
    public async Task<IActionResult> DataUpdate(
        [FromRoute] Guid id,
        [FromBody] DataUpdateRequest request)
    {
        var updated = await dataService.UpdateDataAsync(id, request.Name, request.Flag).ConfigureAwait(false);
        return updated ? Results.NoContent() : Results.NotFound();
    }

    [HttpEndpoint("delete", "data/{id:guid}")]
    public async Task<IActionResult> DataDelete([FromRoute] Guid id)
    {
        var deleted = await dataService.DeleteDataAsync(id).ConfigureAwait(false);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
