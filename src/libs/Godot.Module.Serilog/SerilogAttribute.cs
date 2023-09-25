using Rougamo;
using Rougamo.Context;
using Serilog.Events;
using Serilog;
using System.IO;

namespace Godot.Module.Serilog
{
    public class SerilogAttribute : ExMoAttribute
    {
        private readonly string _path;

        public SerilogAttribute(string floder) 
            => _path = Path.Combine(Path.Combine(Path.GetTempPath(), floder), "logs.txt");

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
            .WriteTo.Async(c => c.File(_path))
            .WriteTo.Async(c => c.GodotPrint())
            .CreateLogger();
        }
    }
}
