namespace HyperEfficient.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCamadaInfra(this IServiceCollection services)
    {
        RegisterBySuffix(services, "HyperEfficient.Repository", "Repository", ServiceLifetime.Transient);
        return services;
    }

    public static IServiceCollection AddCamadaAplicacao(this IServiceCollection services)
    {
        RegisterBySuffix(services, "HyperEfficient.Services", "Service", ServiceLifetime.Transient);
        return services;
    }

    private static void RegisterBySuffix(IServiceCollection services, string assemblyPrefix, string suffix, ServiceLifetime lifetime)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName != null && a.FullName.StartsWith(assemblyPrefix));

        foreach (var assembly in assemblies)
        {
            var implementations = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith(suffix));

            foreach (var impl in implementations)
            {
                var contrato = impl.GetInterfaces()
                    .SingleOrDefault(i => $"I{impl.Name}" == i.Name);
                if (contrato != null)
                    services.Add(new ServiceDescriptor(contrato, impl, lifetime));
            }
        }
    }
}
