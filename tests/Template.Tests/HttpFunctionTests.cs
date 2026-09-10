namespace Template;

using Microsoft.Extensions.Primitives;

using Template.Functions.Functions;
using Template.Testing;

public sealed class HttpFunctionTests
{
    [Fact]
    public async Task HelloReturnsOk()
    {
        var services = HandlerTestHost.CreateServices();
        var request = HandlerTestHost.CreateRequest(
            services,
            query: new Dictionary<string, StringValues> { ["name"] = "world" });

        var result = await HttpFunction.Hello_Handler(request, HandlerTestHost.CreateContext(services)).ConfigureAwait(true);

        Assert.Equal(200, HandlerTestHost.StatusOf(result));
        var value = HandlerTestHost.ValueOf<string>(result);
        Assert.NotNull(value);
        Assert.EndsWith("Hello, world.", value, StringComparison.Ordinal);
    }

    [Fact]
    public async Task HelloWithoutNameReturnsBadRequest()
    {
        var services = HandlerTestHost.CreateServices();
        var request = HandlerTestHost.CreateRequest(services);

        var result = await HttpFunction.Hello_Handler(request, HandlerTestHost.CreateContext(services)).ConfigureAwait(true);

        Assert.Equal(400, HandlerTestHost.StatusOf(result));
    }
}
