namespace HyperEfficient.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCamadaInfra(this IServiceCollection services)
        {
            RegisterBySuffix(
                services,
                targetNamespace: "HyperEfficient.Repositories",
                suffix: "Repository",
                lifetime: ServiceLifetime.Transient);

            return services;
        }

        public static IServiceCollection AddCamadaAplicacao(this IServiceCollection services)
        {
            RegisterBySuffix(
                services,
                targetNamespace: "HyperEfficient.Services",
                suffix: "Service",
                lifetime: ServiceLifetime.Transient);

            return services;
        }

        // ------------------------------------------------------------------
        // Registra automaticamente qualquer classe que termine com <suffix>
        // e cuja interface correspondente siga o padrão I<Classe>.
        // ------------------------------------------------------------------
        private static void RegisterBySuffix(
            IServiceCollection services,
            string targetNamespace,
            string suffix,
            ServiceLifetime lifetime)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t =>
                    t.IsClass &&
                    !t.IsAbstract &&
                    t.Namespace != null &&
                    t.Namespace.StartsWith(targetNamespace) &&
                    t.Name.EndsWith(suffix));

            foreach (var impl in types)
            {
                var contract = impl.GetInterfaces()
                                   .SingleOrDefault(i => $"I{impl.Name}" == i.Name);

                if (contract != null)
                    services.Add(new ServiceDescriptor(contract, impl, lifetime));
            }
        }
    }
}
