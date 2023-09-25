using Godot.Module.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NumberPlace.EntityFrameworkCore
{
    internal class SqliteActionWrapper : OptionsActionWrapper
    {
        private static readonly string _floder = nameof(NumberPlace);

        public override Action<IServiceProvider, DbContextOptionsBuilder>? OptionsAction { get; } = ConfigureSqlite;

        private static void ConfigureSqlite(IServiceProvider provider, DbContextOptionsBuilder builder)
            => builder.UseSqlite($"Data Source={Path.Combine(Path.Combine(Path.GetTempPath(), _floder), "NumberPlace.db")}");
    }
}
