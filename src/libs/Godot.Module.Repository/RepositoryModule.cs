using Microsoft.Extensions.DependencyInjection;
using Godot.Module.Repository.Abstracts;

namespace Godot.Module.Repository
{
    public class RepositoryModule : CustomModule
    {
        public override void ConfigureServices(IServiceCollection services)
            => services.AddSingleton(typeof(IRepository<,>), typeof(Repository<,>))
                .AddSingleton(typeof(IReadOnlyRepository<,>), typeof(ReadOnlyRepository<,>))
                .AddSingleton(typeof(IBasicRepository<,>), typeof(BasicRepository<,>));
    }
}