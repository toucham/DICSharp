using System.Reflection;
using DICSharpDev.Lib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DIRegisterServices
{
    public static class ServiceConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, bool multiAssembly = false, Assembly? currentAssembly = null, Assembly[]? assemblyInputs = null)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            var logger = loggerFactory.CreateLogger("ServiceConfiguration");

            List<Assembly> assemblies = new();
            assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies()); // current assembly

            // support for multi-assemblies project
            if (multiAssembly)
            {
                if (currentAssembly != null)
                {
                    var refAssemblies = currentAssembly.GetReferencedAssemblies()
                        .Where(a => a.GetPublicKeyToken() != null && a.GetPublicKeyToken()?.Length == 0)
                        .Where(a => Assembly.GetExecutingAssembly().GetName().FullName != a.FullName);
                    assemblies.AddRange(from name in refAssemblies select Assembly.Load(name));
                }
                else
                {

                    var dir = Directory.GetCurrentDirectory();
                    Console.WriteLine(dir);
                    var dllFiles = from dlls in Directory.EnumerateFiles(dir, "*.dll", SearchOption.AllDirectories) select dlls.Replace(".dll", "");
                    assemblies.AddRange(from dll in dllFiles select Assembly.LoadFrom(dll));
                }
            }
            if (assemblyInputs != null)
            {
                assemblies.AddRange(assemblyInputs);
            }

            logger.LogInformation("Start adding services...");
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
                            Console.WriteLine($"     {type.Name} -> {inter.Name} ({diLifetime.lifetime})");
                            var descriptor = new ServiceDescriptor(inter, type, diLifetime.lifetime);
                            services.Add(descriptor);
                        }
                    }
                }
            }
        }

    }
}
