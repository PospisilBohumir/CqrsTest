namespace Blazor.MediatR.Client;

internal static class InterfaceMappingHelper
{
    public static IEnumerable<(Type iFace, Type implementation)> MappingByConventionBasedOnInterface(
        Type interfaceType,
        Type? pivot = null)
    {
        return TypesFromAssembly(pivot ?? interfaceType)
            .Where(o => o.IsAssignableToGenericType(interfaceType))
            .Select(o => new
            {
                Type = o,
                iFace = o.GetInterfaces().Single(x => x.IsAssignableToGenericType(interfaceType))
            }).Select(o => (o.iFace, o.Type));
    }

    private static IEnumerable<Type> TypesFromAssembly(params Type[] pivots) => pivots.Select(o => o.Assembly)
        .SelectMany(a => (IEnumerable<Type>)a.GetExportedTypes()).Where(x => !x.IsAbstract && x.IsClass);

    private static bool IsAssignableToGenericType(this Type givenType, Type genericType)
    {
        if (!genericType.IsGenericType)
            return genericType.IsAssignableFrom(givenType);
        if (givenType.GetInterfaces().Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType) ||
            givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            return true;
        var baseType = givenType.BaseType;
        return baseType != null && baseType.IsAssignableToGenericType(genericType);
    }
}