using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Godot.Module.DependencyInjection
{
    internal static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetEntryAssembly()
                ?? throw new ArgumentNullException("Cannot resolve EntryAssembly.");
            return services.ResolveFromAttribute(assembly);
        }

        private static IServiceCollection ResolveFromAttribute(this IServiceCollection services, Assembly assembly)
        {
            foreach (var item in assembly.GetTypes())
            {
                foreach(var attribute in item.GetCustomAttributes())
                {
                    SingletonAttribute? singleton = attribute as SingletonAttribute;
                    if (singleton is not null)
                    {
                        if (singleton.ServiceType is not null)
                            services.AddSingleton(singleton.ServiceType, item);
                        else
                            services.AddSingleton(item);
                        break;
                    }

                    ScopedAttribute? scoped = attribute as ScopedAttribute;
                    if (scoped is not null)
                    {
                        if (scoped.ServiceType is not null)
                            services.AddScoped(scoped.ServiceType, item);
                        else
                            services.AddScoped(item);
                        break;
                    }

                    TransientAttribute? transient = attribute as TransientAttribute;
                    if (transient is not null)
                    {
                        if (transient.ServiceType is not null)
                            services.AddTransient(transient.ServiceType, item);
                        else
                            services.AddTransient(item);
                        break;
                    }
                }
            }
            return services;
        }
    }
}
