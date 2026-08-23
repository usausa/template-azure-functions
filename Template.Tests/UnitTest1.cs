namespace Template;

public sealed class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var service = new Service(TimeProvider.System);
        Assert.NotEmpty(service.GetTimestamp());
    }
}
