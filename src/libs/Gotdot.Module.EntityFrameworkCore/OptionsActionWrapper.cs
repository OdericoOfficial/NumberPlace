using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Godot.Module.EntityFrameworkCore
{
    public abstract class OptionsActionWrapper
    {
        public abstract Action<IServiceProvider, DbContextOptionsBuilder>? OptionsAction { get; }

        public virtual ServiceLifetime ContextLifetime { get; } = ServiceLifetime.Singleton;

        public virtual ServiceLifetime OptionsLifetime { get; } = ServiceLifetime.Singleton;
    }
}
