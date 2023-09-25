using Microsoft.Extensions.DependencyInjection;

namespace Godot.Module
{
    public abstract class CustomModule
    {
        public virtual ValueTask ConfigureServicesAsync(IServiceCollection services)
        {
            ConfigureServices(services);
            return ValueTask.CompletedTask;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {

        }

        public virtual ValueTask OnApplicationInitializationAsync(IServiceProvider provider)
        {
            OnApplicationInitialization(provider);
            return ValueTask.CompletedTask;
        }

        public virtual void OnApplicationInitialization(IServiceProvider provider)
        {

        }
    }
}
