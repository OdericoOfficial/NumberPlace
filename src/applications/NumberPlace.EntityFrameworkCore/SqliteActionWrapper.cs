using Godot.Module.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace NumberPlace.EntityFrameworkCore
{
    internal class SqliteActionWrapper : OptionsActionWrapper
    {
        public static readonly string DatabasePath = Path.Combine(Path.Combine(Path.GetTempPath(), nameof(NumberPlace), "NumberPlace.db"));

        public override Action<IServiceProvider, DbContextOptionsBuilder>? OptionsAction { get; } = ConfigureSqlite;

        private static void ConfigureSqlite(IServiceProvider provider, DbContextOptionsBuilder builder)
            => builder.UseSqlite($"Data Source={DatabasePath}");
    }
}
