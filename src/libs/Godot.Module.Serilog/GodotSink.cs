using Serilog.Core;
using Serilog.Events;


namespace Godot.Module.Serilog
{
    internal class GodotSink : ILogEventSink
    {
        public void Emit(LogEvent logEvent)
            => GD.Print($"{logEvent.Timestamp} [{logEvent.Level}] {logEvent.RenderMessage()}");
    }
}
