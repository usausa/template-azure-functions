namespace Template.Functions.Infrastructure;

using System.Text.Json;

using Microsoft.AspNetCore.Http;

public static class RequestValidator
{
    public static async ValueTask<(T? Request, IResult? Error)> ParseAsync<T>(HttpRequest request)
        where T : class
    {
        T? value;
        try
        {
            value = await request.ReadFromJsonAsync<T>(request.HttpContext.RequestAborted).ConfigureAwait(false);
        }
        catch (JsonException)
        {
            return (null, Results.BadRequest());
        }

        if (value is null)
        {
            return (null, Results.BadRequest());
        }

        var results = new List<ValidationResult>();
        if (!Validator.TryValidateObject(value, new ValidationContext(value), results, validateAllProperties: true))
        {
            var errors = results
                .SelectMany(static x => x.MemberNames.DefaultIfEmpty(string.Empty), static (x, member) => new { Member = member, x.ErrorMessage })
                .GroupBy(static x => x.Member, static x => x.ErrorMessage ?? string.Empty)
                .ToDictionary(static x => x.Key, static x => x.ToArray());
            return (null, Results.ValidationProblem(errors));
        }

        return (value, null);
    }
}
