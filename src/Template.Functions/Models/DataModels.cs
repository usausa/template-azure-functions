namespace Template.Functions.Models;

public sealed record DataListEntry(Guid Id, string Name, bool Flag, DateTime UpdateAt);

public sealed record DataListResponse(int Total, IReadOnlyList<DataListEntry> Items);

public sealed record DataGetResponse(Guid Id, string Name, bool Flag, DateTime UpdateAt);

public sealed record DataCreateRequest(
    [property: Required][property: MaxLength(50)] string Name,
    bool Flag);

public sealed record DataCreateResponse(Guid Id);

public sealed record DataUpdateRequest(
    [property: Required][property: MaxLength(50)] string Name,
    bool Flag);
