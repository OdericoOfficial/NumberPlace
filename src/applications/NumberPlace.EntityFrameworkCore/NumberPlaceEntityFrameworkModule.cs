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
        public override void OnApplicationInitialization(IServiceProvider provider)
        {
            if (!File.Exists(SqliteActionWrapper.DatabasePath))
                provider.GetRequiredService<DbContext>().Database.Migrate();
        }
    }
}
