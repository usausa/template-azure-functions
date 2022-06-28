namespace Template.Services;

public sealed class Service
{
#pragma warning disable CA1822
    public string GetTimestamp() => DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.CurrentInfo);
#pragma warning restore CA1822
}
