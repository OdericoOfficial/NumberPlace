using Microsoft.EntityFrameworkCore;
using NumberPlace.Domain;

namespace NumberPlace.EntityFrameworkCore
{
    internal class NumberPlaceDbContext : DbContext
    {
        public NumberPlaceDbContext(DbContextOptions<NumberPlaceDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.Entity<NumberMat>().HasKey(x => x.Id);
    }
}
