using Rougamo;
using Rougamo.Context;
using Serilog.Events;
using Serilog;

namespace Godot.Module.Serilog
{
    public class SerilogAttribute : ExMoAttribute
    {
        protected override void ExOnEntry(MethodContext context)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File(Path.Combine(Path.Combine(Path.GetTempPath(), "Log"), "logs.txt")))
            .WriteTo.Async(c => c.GodotPrint())
            .CreateLogger();
        }
    }
}
