namespace Template.Functions.Mappers;

using Smart.Mapper;

using Template.Functions.Models;
using Template.Models;

internal static partial class DataMapper
{
    [Mapper]
    public static partial DataResponse ToResponse(this DataEntity entity);
}
