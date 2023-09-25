using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Godot.Module.EntityFrameworkCore
{
    public class EntityFrameworkCoreModule<TDbContext, TWrapper> : CustomModule
        where TDbContext : DbContext where TWrapper : OptionsActionWrapper, new()
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            OptionsActionWrapper wrapper = new TWrapper();
            services.AddDbContext<DbContext, TDbContext>(wrapper.OptionsAction, wrapper.ContextLifetime, wrapper.OptionsLifetime);
        }
    }
}