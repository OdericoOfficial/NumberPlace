using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Godot.Module.Serilog
{
    public class SerilogModule : CustomModule
    {
        public override void ConfigureServices(IServiceCollection services)
            => services.AddLogging(config => config.AddSerilog(dispose: true));
    }
}