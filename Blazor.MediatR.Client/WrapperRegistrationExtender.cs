using MediatR;
using MediatR.Registration;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.MediatR.Client;

public static class WrapperRegistrationExtender
{
    private static readonly BlazorWrapperSetup DefaultSetup = new("/CQRS");

    public static IServiceCollection AddBlazorMediatRClient(this IServiceCollection services, params Type[] pivots)
        => AddBlazorMediatRClient(services, DefaultSetup, new MediatRServiceConfiguration(), pivots);

    public static IServiceCollection AddBlazorMediatRClient(this IServiceCollection services, BlazorWrapperSetup blazorWrapperSetup,
        MediatRServiceConfiguration mediatRServiceConfiguration, params Type[] pivots)
    {
        ServiceRegistrar.AddRequiredServices(services, mediatRServiceConfiguration);
        return services.AddMediatorWrappers(blazorWrapperSetup, pivots);
    }

    public static IServiceCollection AddMediatorWrappers(this IServiceCollection services, BlazorWrapperSetup blazorWrapperSetup,
        params Type[] pivots)
    {
        foreach (var (iFace, requestType) in pivots.SelectMany(o =>
                     InterfaceMappingHelper.MappingByConventionBasedOnInterface(typeof(IRequest<>), o)))
        {
            var returnValueType = iFace.GenericTypeArguments.Single();
            var wrapperType = typeof(BlazorWrapperRequestHandler<,>).MakeGenericType(requestType, returnValueType);
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, returnValueType);
            services.AddScoped(handlerType, wrapperType);
        }

        services.AddSingleton<IBlazorWrapperSetup>(blazorWrapperSetup);
        return services;
    }
}