using Microsoft.AspNetCore.Builder;
using System.Text.Json;

namespace Blazor.MediatR.Server;

public static class BlazorMediatRServerExtensions
{
    public static IApplicationBuilder UseBlazorMediatRServer(this IApplicationBuilder app) =>
        UseBlazorMediatRServer(app, new BlazorWrapperSetup("/CQRS", new JsonSerializerOptions(JsonSerializerDefaults.Web)));

    public static IApplicationBuilder UseBlazorMediatRServer(this IApplicationBuilder app, BlazorWrapperSetup setup) 
        => app.UseMiddleware<MediatorCqrsMiddleware>(setup);
}