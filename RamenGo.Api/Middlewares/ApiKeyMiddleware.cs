using RamenGo.Api.DTOs;
using System.Text.Json;

namespace RamenGo.Api.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _apiKey = config["Api-Key:x-api-key"]!;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.ContentType = "application/json";

            var endpoint = context.GetEndpoint();
            bool authorizeRequest = endpoint?.Metadata?.GetMetadata<ApiKeyAuthorize>() != null;

            if (authorizeRequest)
            {
                if (!context.Request.Headers.TryGetValue("x-api-key", out var apiKeyHeader))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(new ErrorResponse("x-api-key header missing")));
                    return;
                }

                string apiKey = apiKeyHeader.First()!;
                if (!apiKey.Equals(_apiKey))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse("invalid x-api-key")));
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }
}
