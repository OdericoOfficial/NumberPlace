using Microsoft.Extensions.DependencyInjection;

namespace Godot.Module
{
    public abstract class CustomModule
    {
        public virtual void ConfigureServices(IServiceCollection services)
        {

        }

        public virtual void OnApplicationInitialization(IServiceProvider provider)
        {

        }
    }
}
