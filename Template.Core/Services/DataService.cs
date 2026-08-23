namespace Template.Services;

using Smart.Data.Accessor;

using Template.Accessors;
using Template.Models;

public sealed class DataService
{
    private readonly IDataAccessor dataAccessor;

    private readonly TimeProvider timeProvider;

    public DataService(
        IAccessorResolver<IDataAccessor> dataAccessor,
        TimeProvider timeProvider)
    {
        this.dataAccessor = dataAccessor.Accessor;
        this.timeProvider = timeProvider;
    }

    public ValueTask<int> CountDataAsync(bool? flag) =>
        dataAccessor.CountDataAsync(flag);

    public ValueTask<List<DataEntity>> QueryDataListAsync(bool? flag, int limit, int offset) =>
        dataAccessor.QueryDataListAsync(flag, limit, offset);

    public ValueTask<DataEntity?> QueryDataAsync(Guid id) =>
        dataAccessor.QueryDataAsync(id);

    public async ValueTask<Guid> InsertDataAsync(string name, bool flag)
    {
        var entity = new DataEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            Flag = flag,
            UpdateAt = timeProvider.GetLocalNow().DateTime
        };
        await dataAccessor.InsertAsync(entity).ConfigureAwait(false);
        return entity.Id;
    }

    public async ValueTask<bool> UpdateDataAsync(Guid id, string name, bool flag)
    {
        var entity = new DataEntity
        {
            Id = id,
            Name = name,
            Flag = flag,
            UpdateAt = timeProvider.GetLocalNow().DateTime
        };
        var rows = await dataAccessor.UpdateAsync(entity).ConfigureAwait(false);
        return rows > 0;
    }

    public async ValueTask<bool> DeleteDataAsync(Guid id)
    {
        var rows = await dataAccessor.DeleteAsync(new DataEntity { Id = id }).ConfigureAwait(false);
        return rows > 0;
    }
}
