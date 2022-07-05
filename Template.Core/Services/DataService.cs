namespace Template.Services;

using Smart.Data.Accessor;

using Template.Accessors;
using Template.Models;

public class DataService
{
    private readonly IDataAccessor dataAccessor;

    public DataService(IAccessorResolver<IDataAccessor> dataAccessor)
    {
        this.dataAccessor = dataAccessor.Accessor;
    }

    public ValueTask<List<DataEntity>> QueryDataListAsync(bool? flag, int limit, int offset) =>
        dataAccessor.QueryDataListAsync(flag, limit, offset);
}
