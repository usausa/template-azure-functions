namespace Template;

using Microsoft.Extensions.Primitives;

using Template.Functions.Functions;
using Template.Testing;

public sealed class DataFunctionTests
{
    [Theory]
    [InlineData("0", "0")]
    [InlineData("201", "0")]
    [InlineData("10", "-1")]
    public async Task DataQueryListWithInvalidRangeReturnsBadRequest(string limit, string offset)
    {
        var services = HandlerTestHost.CreateServices();
        var request = HandlerTestHost.CreateRequest(
            services,
            query: new Dictionary<string, StringValues> { ["limit"] = limit, ["offset"] = offset });

        var result = await DataFunction.DataQueryList_Handler(request, HandlerTestHost.CreateContext(services)).ConfigureAwait(true);

        Assert.Equal(400, HandlerTestHost.StatusOf(result));
    }
}
