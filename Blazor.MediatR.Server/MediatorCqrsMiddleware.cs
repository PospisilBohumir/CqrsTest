using System.Text.Json;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Blazor.MediatR.Server;

public class MediatorCqrsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly BlazorWrapperSetup _blazorWrapperSetup;
    
    public MediatorCqrsMiddleware(RequestDelegate next, BlazorWrapperSetup blazorWrapperSetup)
    {   
        _next = next;
        _blazorWrapperSetup = blazorWrapperSetup;
    }

    public async Task Invoke(HttpContext httpContext, IMediator mediator)
    {
        var request = httpContext.Request;
        if (request.Path == _blazorWrapperSetup.MediatorPath && request.Method == HttpMethods.Post)
        {
            using var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            var messageContract = JsonSerializer.Deserialize<MessageContract>(body, new JsonSerializerOptions(JsonSerializerDefaults.Web))!;

            var type = Type.GetType(messageContract.ObjectName)!;
            var r = JsonSerializer.Deserialize(messageContract.Json, type)!;


            var result = await mediator.Send(r);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 200;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(result));
        }
        else
        {
            await _next(httpContext);
        }
    }
}