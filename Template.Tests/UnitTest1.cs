namespace Template;

public sealed class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var service = new Service();
        Assert.NotEmpty(service.GetTimestamp());
    }
}
