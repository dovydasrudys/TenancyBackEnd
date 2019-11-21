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
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Failure> Failures { get; set; }
    }
}
