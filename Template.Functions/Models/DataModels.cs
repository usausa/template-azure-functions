namespace Template.Functions.Models;

public sealed record DataResponse(Guid Id, string Name, bool Flag, DateTime UpdateAt);

public sealed record DataListResponse(int Total, IReadOnlyList<DataResponse> Items);

public sealed record DataInsertRequest(
    [property: Required][property: MaxLength(50)] string Name,
    bool Flag);

public sealed record DataInsertResponse(Guid Id);

public sealed record DataUpdateRequest(
    [property: Required][property: MaxLength(50)] string Name,
    bool Flag);
