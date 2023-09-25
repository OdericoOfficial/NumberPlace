using Godot.Module;
using Godot.Module.DependencyInjection;
using Godot.Module.Serilog;
using Microsoft.Extensions.DependencyInjection;
using NumberPlace.Algorithm;
using NumberPlace.EntityFrameworkCore;

namespace NumberPlace.Application
{
    [DependsOn(typeof(SerilogModule),
        typeof(NumberPlaceEntityFrameworkModule),
        typeof(InjectServiceModule))]
    public class NumberPlaceApplicationModule : CustomModule
    {
        [Serilog(nameof(NumberPlace))]
        public override void ConfigureServices(IServiceCollection services)
            => services.AddMatFactory();
    }
}
