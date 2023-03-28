using Microsoft.AspNetCore.Builder;

namespace Blazor.MediatR.Server;

public static class BlazorMediatRServerExtensions
{
    public static IApplicationBuilder UseBlazorMediatRServer(this IApplicationBuilder app) =>
        UseBlazorMediatRServer(app, new BlazorWrapperSetup("/CQRS"));

    public static IApplicationBuilder UseBlazorMediatRServer(this IApplicationBuilder app, BlazorWrapperSetup setup) 
        => app.UseMiddleware<MediatorCqrsMiddleware>(setup);
}