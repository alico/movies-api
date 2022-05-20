using Movies.Application.Common.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Movies.API.Middlewares;
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger)
    {
        string requestId = GetRequestId(context);
        context.Items["CorrelationId"] = requestId;
        await Next(context, logger, requestId);

    }

    private async Task Next(HttpContext context, ILogger<ExceptionHandlingMiddleware> logger, string requestId)
    {
        try
        {
            await _next(context);
        }
        catch (CustomException ex)
        {
            logger.LogError(ex, "{Message}{RequestId}", ex.Message, requestId);
            var messages = new List<string>() { ex.Message };
            await HandleExceptionAsync(context, requestId, messages);
        }

        catch (CustomValidationException ex)
        {
            var messages = new List<string>();
            foreach (var error in ex.Errors)
            {
                messages.Add(string.Format("{0} {1}", error.Key, string.Join(',', error.Value)));
            }
            await HandleExceptionAsync(context, requestId, messages);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Message}{RequestId}", ex.Message, requestId);
            var messages = new List<string>() { "Error while processing request." };
            await HandleExceptionAsync(context, requestId, messages);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, string correlationId, List<string> message)
    {
        context.Response.Clear();
        var apiResponse = new ErrorResponseWrapper<ErrorResponse>();
        var apiError = new ErrorResponse(message);
        apiError.CorrelationId = correlationId;
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";
        apiResponse.Error = apiError;
        var json = JsonConvert.SerializeObject(apiResponse);

        return context.Response.WriteAsync(json);
    }

    private static string GetRequestId(HttpContext context)
    {
        string requestId;
        var header = context.Request.Headers["CorrelationId"];
        if (header.Count > 0)
            requestId = header[0];
        else
            requestId = Guid.NewGuid().ToString();

        return requestId;
    }
}
