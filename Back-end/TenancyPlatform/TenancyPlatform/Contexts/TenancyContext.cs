using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenancyPlatform.Models;

namespace TenancyPlatform.Contexts
{
    public class TenancyContext : DbContext
    {
        public TenancyContext()
        {
        }
        public TenancyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RealEstate> RealEstates { get; set; }
    }
}
