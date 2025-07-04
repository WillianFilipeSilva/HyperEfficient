namespace HyperEfficient.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCamadaInfra(this IServiceCollection services)
        {
            RegisterBySuffix(services, "HyperEfficient.Repositories", "Repository", ServiceLifetime.Transient);

            return services;
        }

        public static IServiceCollection AddCamadaAplicacao(this IServiceCollection services)
        {
            RegisterBySuffix(services, "HyperEfficient.Services", "Service", ServiceLifetime.Transient);

            return services;
        }

        // ------------------------------------------------------------------
        // Registra automaticamente qualquer classe que termine com <suffix>
        // e cuja interface correspondente siga o padrão I<Classe>.
        // ------------------------------------------------------------------
        private static void RegisterBySuffix(IServiceCollection services, string targetNamespace, string suffix, ServiceLifetime lifetime)
        {
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).Where(t => t.IsClass && !t.IsAbstract && t.Namespace != null && t.Namespace.StartsWith(targetNamespace) && t.Name.EndsWith(suffix));

            foreach (Type impl in types)
            {
                Type? contract = impl.GetInterfaces().SingleOrDefault(i => $"I{impl.Name}" == i.Name);

                if (contract != null)
                {
                    services.Add(new ServiceDescriptor(contract, impl, lifetime));
                }
            }
        }
    }
}