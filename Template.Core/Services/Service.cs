namespace Template.Services;

public sealed class Service
{
    private readonly TimeProvider timeProvider;

    public Service(TimeProvider timeProvider)
    {
        this.timeProvider = timeProvider;
    }

    public string GetTimestamp() => timeProvider.GetLocalNow().ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.CurrentInfo);
}
