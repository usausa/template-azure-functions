namespace Template;

public sealed class ServiceTests
{
    [Fact]
    public void GetTimestampReturnsValue()
    {
        var service = new Service(TimeProvider.System);
        Assert.NotEmpty(service.GetTimestamp());
    }
}
