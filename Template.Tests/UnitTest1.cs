namespace Template;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var service = new Service();
        Assert.NotEmpty(service.GetTimestamp());
    }
}
