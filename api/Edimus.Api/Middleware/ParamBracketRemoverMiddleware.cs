namespace Edimus.Api.Middleware;

public class ParamBracketRemoverMiddleware
{
    private readonly RequestDelegate _next;
    public ParamBracketRemoverMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var request = httpContext.Request;

        request.QueryString = GetQueryStringWithoutBrackets(request.QueryString);

        await _next(httpContext);
    }

    public QueryString GetQueryStringWithoutBrackets(QueryString queryString)
    {
        if (!queryString.HasValue) return queryString;

        var queryStringValue = queryString.ToString();

        if (queryStringValue.StartsWith("?"))
            queryStringValue = queryStringValue.Substring(1);
        var queryParams = queryStringValue.Split("&");

        List<KeyValuePair<string, string>> list =
        new(queryParams.Select(param =>
        {
            var kvp = param.Split('=');
            var newParamName = kvp[0].Replace("[]", "");
            return new KeyValuePair<string, string>(newParamName, kvp[1]);
        }));

        var newQueryString = QueryString.Create(list!);

        return newQueryString;
    }
}
