using Serilog.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godot.Module.Serilog
{
    internal static class GodotSinkExtensions
    {
        public static LoggerConfiguration GodotPrint(this LoggerSinkConfiguration sinkConfiguration)
            => sinkConfiguration.Sink(new GodotSink());
    }
}
