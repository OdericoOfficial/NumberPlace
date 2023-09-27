using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.EntityFrameworkCore
{
    internal class NumberPlaceDbContextFactory : IDesignTimeDbContextFactory<NumberPlaceDbContext>
    {
        public NumberPlaceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NumberPlaceDbContext>();
            optionsBuilder.UseSqlite($"Data Source={SqliteActionWrapper.DatabasePath}");
            return new NumberPlaceDbContext(optionsBuilder.Options);
        }
    }
}
