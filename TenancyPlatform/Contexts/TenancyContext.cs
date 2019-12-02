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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RealEstate>().HasData(
                new RealEstate
                {
                    Id = 1,
                    Country = "Lithuania",
                    City = "Kaunas",
                    Street = "Veiveriu g.",
                    HouseNr = "7",
                    Floor = 1,
                    Area = 80,
                    BuildYear = 2007,
                    Rooms = 3
                },
                new RealEstate
                {
                    Id = 2,
                    Country = "Lithuania",
                    City = "Kaunas",
                    Street = "Taikos pr.",
                    HouseNr = "56A",
                    Floor = 6,
                    Area = 45,
                    BuildYear = 1999,
                    Rooms = 1
                },
                new RealEstate
                {
                    Id = 3,
                    Country = "Lithuania",
                    City = "Vilnius",
                    Street = "Šimulionio g.",
                    HouseNr = "3",
                    Floor = 2,
                    Area = 60,
                    BuildYear = 2011,
                    Rooms = 2
                }
            );

            modelBuilder.Entity<Advert>().HasData(
                new Advert
                {
                    Id = 1,
                    Description = "Puikus butas. Arti mokykla.",
                    LoanPrice = 300,
                    OwnerId = 1,
                    RealEstateId = 1
                },
                new Advert
                {
                    Id = 2,
                    Description = "",
                    LoanPrice = 220,
                    OwnerId = 2,
                    RealEstateId = 2
                },
                new Advert
                {
                    Id = 3,
                    Description = "Išnuomuojame ilgalaikei nuomai butą varpų 11-36, šalia prekybos centro, butas labai šiltas ir sauletas, rakinama laiptine, patogus susisiekimas aplink daug mokyklų, su baldais.",
                    LoanPrice = 230,
                    OwnerId = 3,
                    RealEstateId = 3
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Antanas",
                    LastName = "Antanaitis",
                    UserName = "antanas@gmail.com",
                    Password = "antanas",
                    Role = "landlord"
                },
                new User
                {
                    Id = 2,
                    FirstName = "Jonas",
                    LastName = "Jonaitis",
                    UserName = "jonas@gmail.com",
                    Password = "jonas",
                    Role = "landlord"
                },
                new User
                {
                    Id = 3,
                    FirstName = "Dovydas",
                    LastName = "Dovydaitis",
                    UserName = "dovydas@gmail.com",
                    Password = "dovydas",
                    Role = "tenant"
                },
                new User
                {
                    Id = 4,
                    FirstName = "Julius",
                    LastName = "Julaitis",
                    UserName = "julius@gmail.com",
                    Password = "julius",
                    Role = "tenant"
                }
            );

            modelBuilder.Entity<Message>().HasData(
                new Message
                {
                    Id = 1,
                    Date = new DateTime(2019,01,01,8,0,0),
                    ReceiverId = 1,
                    SenderId = 3,
                    Content = "Sveiki, mane domina jūsų nuomojamas butas. Ar būtų galimybė apžiūrėti butą ateinančią savaitę?"
                },
                new Message
                {
                    Id = 2,
                    Date = new DateTime(2019, 01, 01, 8, 24, 13),
                    ReceiverId = 3,
                    SenderId = 1,
                    Content = "Laba diena. Susitikti galime trečiadienį."
                },
                new Message
                {
                    Id = 3,
                    Date = new DateTime(2019, 01, 01, 11, 37, 48),
                    ReceiverId = 1,
                    SenderId = 3,
                    Content = "Ačiū. Iki"
                },
                new Message
                {
                    Id = 4,
                    Date = new DateTime(2019, 01, 01, 8, 0, 0),
                    ReceiverId = 2,
                    SenderId = 3,
                    Content = "Sveiki, mane domina jūsų nuomojamas butas. Ar būtų galimybė apžiūrėti butą ateinančią savaitę?"
                }
            );

            modelBuilder.Entity<Contract>().HasData(
                new Contract
                {
                    Id = 1,
                    LandlordId = 1,
                    TenantId = 3,
                    RealEstateId = 1,
                    Start = new DateTime(2019, 02, 01),
                    Duration = 12,
                    Price = 300
                }
            );

            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    Amount = 15,
                    Description = "Internetas už 2019 02 mėn.",
                    PaymentId = 1
                },
                new Service
                {
                    Id = 2,
                    Amount = 50,
                    Description = "Šildymas už 2019 02 mėn.",
                    PaymentId = 1
                },
                new Service
                {
                    Id = 3,
                    Amount = 26,
                    Description = "Elektra už 2019 02 mėn.",
                    PaymentId = 1
                },
                new Service
                {
                    Id = 4,
                    Amount = 123.15,
                    Description = "Komunaliniai už 2019 03 mėn.",
                    PaymentId = 2
                }
            );

            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    Id = 1,
                    ContractId = 1,
                    PaymentStatus = PaymentStatus.Paid
                },
                new Payment
                {
                    Id = 2,
                    ContractId = 1,
                    PaymentStatus = PaymentStatus.NotPaid
                }
            );

            modelBuilder.Entity<Failure>().HasData(
                new Failure
                {
                    Id = 1,
                    ContractId = 1,
                    Description = "Sugedusi durų spyna",
                    ReporterId = 3,
                    IsFixed = false,
                }
            );
        }
    }
}
