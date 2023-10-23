using System.Reflection;
using DICSharpDev.Lib;
using Microsoft.Extensions.DependencyInjection;

namespace DIRegisterServices
{
    public static class ServiceConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, string[]? assemblyFiles = null, Assembly[]? assemblyInputs = null)
        {
            List<Assembly> assemblies = new();
            assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies()); // current assembly

            // support for multi-assemblies project
            if (assemblyFiles != null)
            {
                assemblies.AddRange(from file in assemblyFiles
                                    select Assembly.LoadFrom(file));
            }
            if (assemblyInputs != null)
            {
                assemblies.AddRange(assemblyInputs);
            }

            // register services according to lifetime
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var diLifetime = type.GetCustomAttribute<DILifetimeAttribute>();
                    var interfaces = type.GetInterfaces();
                    if (diLifetime != null)
                    {
                        foreach (var inter in interfaces)
                        {
                            var descriptor = new ServiceDescriptor(inter, type, diLifetime.lifetime);
                            services.Add(descriptor);
                        }
                    }
                }
            }
        }

    }
}
