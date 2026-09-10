namespace Template.Functions.Functions;

using AzureFunctionsExtension;
using AzureFunctionsExtension.Annotations;

using Template.Functions.Models;
using Template.Models;
using Template.Services;

using IActionResult = Microsoft.AspNetCore.Mvc.IActionResult;

[AzureFunction]
public sealed partial class DataFunction
{
    private readonly DataService dataService;

    public DataFunction(DataService dataService)
    {
        this.dataService = dataService;
    }

    //--------------------------------------------------------------------------------
    // Handler
    //--------------------------------------------------------------------------------

    [HttpEndpoint("get", "data")]
    public async Task<IActionResult> DataQueryList(
        [FromQuery] bool? flag,
        [FromQuery] int limit = 10,
        [FromQuery] int offset = 0)
    {
        var total = await dataService.CountDataAsync(flag).ConfigureAwait(false);
        var list = await dataService.QueryDataListAsync(flag, limit, offset).ConfigureAwait(false);

        return Results.Ok(new DataListResponse(total, list.Select(MapToResponse).ToList()));
    }

    [HttpEndpoint("get", "data/{id:guid}")]
    public async Task<IActionResult> DataQuery([FromRoute] Guid id)
    {
        var entity = await dataService.QueryDataAsync(id).ConfigureAwait(false);
        return entity is not null ? Results.Ok(MapToResponse(entity)) : Results.NotFound();
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

    //--------------------------------------------------------------------------------
    // Helper
    //--------------------------------------------------------------------------------

    private static DataResponse MapToResponse(DataEntity entity) =>
        new(entity.Id, entity.Name, entity.Flag, entity.UpdateAt);
}
