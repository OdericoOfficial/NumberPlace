using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberPlace.EntityFrameworkCore
{
    internal class NumberPlaceDbContext : DbContext
    {
        public NumberPlaceDbContext(DbContextOptions<NumberPlaceDbContext> options) : base(options)
        {

        }
    }
}
