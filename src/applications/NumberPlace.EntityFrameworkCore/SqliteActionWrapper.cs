using Godot.Module.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NumberPlace.EntityFrameworkCore
{
    internal class SqliteActionWrapper : OptionsActionWrapper
    {
        public override Action<IServiceProvider, DbContextOptionsBuilder>? OptionsAction { get; } = ConfigureSqlite;

        private static void ConfigureSqlite(IServiceProvider provider, DbContextOptionsBuilder builder)
        {
            builder.UseSqlite();
        }
    }
}
