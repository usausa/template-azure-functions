namespace Template.Accessors;

using Smart.Data.Accessor.Attributes;

using Template.Models;

[DataAccessor]
[TypeMap(typeof(DateTime), DbType.DateTime2)]
public sealed partial class DataAccessor
{
    [ExecuteScalar]
    public partial ValueTask<int> CountDataAsync(bool? flag);

    [Query]
    public partial ValueTask<List<DataEntity>> QueryDataListAsync(bool? flag, int limit, int offset);

    [QueryFirst]
    public partial ValueTask<DataEntity?> QueryDataAsync(Guid id);

    [Execute]
    public partial ValueTask<int> InsertAsync(Guid id, string name, bool flag, DateTime updateAt);

    [Execute]
    public partial ValueTask<int> UpdateAsync(Guid id, string name, bool flag, DateTime updateAt);

    [Execute]
    public partial ValueTask<int> DeleteAsync(Guid id);
}
