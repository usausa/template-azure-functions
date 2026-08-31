namespace Template.Services;

using Template.Accessors;
using Template.Models;

public sealed class DataService
{
    private readonly DataAccessor dataAccessor;

    private readonly TimeProvider timeProvider;

    public DataService(
        DataAccessor dataAccessor,
        TimeProvider timeProvider)
    {
        this.dataAccessor = dataAccessor;
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
        var id = Guid.NewGuid();
        await dataAccessor.InsertAsync(id, name, flag, timeProvider.GetLocalNow().DateTime).ConfigureAwait(false);
        return id;
    }

    public async ValueTask<bool> UpdateDataAsync(Guid id, string name, bool flag)
    {
        var rows = await dataAccessor.UpdateAsync(id, name, flag, timeProvider.GetLocalNow().DateTime).ConfigureAwait(false);
        return rows > 0;
    }

    public async ValueTask<bool> DeleteDataAsync(Guid id)
    {
        var rows = await dataAccessor.DeleteAsync(id).ConfigureAwait(false);
        return rows > 0;
    }
}
