using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godot.Module.Logging
{
    public interface ILoggerProxy
    {
        ILogger Logger { get; }
    }

    public interface ILoggerProxy<T> : ILoggerProxy
    {
        new ILogger<T> Logger { get; set; }
    }
}
