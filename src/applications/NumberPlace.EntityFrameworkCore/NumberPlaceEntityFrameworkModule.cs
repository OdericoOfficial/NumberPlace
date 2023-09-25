using Godot.Module;
using Godot.Module.EntityFrameworkCore;
using Godot.Module.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NumberPlace.EntityFrameworkCore
{
    [DependsOn(typeof(EntityFrameworkCoreModule<NumberPlaceDbContext, SqliteActionWrapper>),
        typeof(RepositoryModule))]
    public class NumberPlaceEntityFrameworkModule : CustomModule
    {
        public override async ValueTask OnApplicationInitializationAsync(IServiceProvider provider)
            => await provider.GetRequiredService<DbContext>().Database.MigrateAsync();
    }
}
