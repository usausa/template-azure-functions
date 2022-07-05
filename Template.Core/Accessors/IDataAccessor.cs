namespace Template.Accessors;

using Smart.Data.Accessor.Attributes;
using Smart.Data.Accessor.Builders;

using Template.Models;

[DataAccessor]
public interface IDataAccessor
{
    [Query]
    ValueTask<List<DataEntity>> QueryDataListAsync(bool? flag, int limit, int offset);

    [ExecuteScalar]
    ValueTask<int> CountDataAsync(bool? flag);

    [Insert]
    ValueTask InsertAsync(DataEntity entity);

    [Update]
    ValueTask<int> UpdateAsync(DataEntity entity);

    [Delete]
    ValueTask<int> DeleteAsync(DataEntity entity);
}
