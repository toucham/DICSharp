using System.Reflection;
using System.Linq;

namespace DICSharp.Lib
{
    public static class ServiceConfiguration
    {
        public static void AddServicesToContainer(this IServiceCollection services, string[]? assemblyFiles = null)
        {
            List<Assembly> assemblies = new();
            assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies()); // current assembly
            if (assemblyFiles != null)
            {
                assemblies.AddRange(from file in assemblyFiles
                                    select Assembly.LoadFrom(file)); // support for multi-assemblies project
            }

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