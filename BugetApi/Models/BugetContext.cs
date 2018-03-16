using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugetApi.Models
{
    public class BugetContext :DbContext
    {
        public BugetContext(DbContextOptions<BugetContext> options) : base(options) { }

        public DbSet<BugetItem> BugetItems { get; set; }

    }
}
