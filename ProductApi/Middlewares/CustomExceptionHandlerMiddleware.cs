using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Shop.EndPoint.Api.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
    public CustomExceptionHandlerMiddleware(RequestDelegate requestDelegate, ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = requestDelegate;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (UnauthorizedAccessException ex)
        {
            //var response = new HttpResponseMessage
            //{
            //    StatusCode = System.Net.HttpStatusCode.Unauthorized,
            //    Headers = new HttpContentHeaders
            //    {
            //        ContentType = new MediaTypeHeaderValue("application/json")
            //    }
            //};
            var response = new ResponseSample(ex.Message, "Green");

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode =(int) HttpStatusCode.Unauthorized;
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
        catch (Exception ex)
        {

            throw;
        }
    }

}

public record ResponseSample
{
    public ResponseSample(string message, string color)
    {
        Message = message;
        Color = color;
    }

    public string Message { get; set; }
    public string Color { get; set; }
}