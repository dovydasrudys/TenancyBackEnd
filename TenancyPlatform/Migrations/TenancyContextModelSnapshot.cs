﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TenancyPlatform.Contexts;

namespace TenancyPlatform.Migrations
{
    [DbContext(typeof(TenancyContext))]
    partial class TenancyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TenancyPlatform.Models.Advert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<double>("LoanPrice");

                    b.Property<int?>("OwnerId");

                    b.Property<int?>("RealEstateId");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("RealEstateId");

                    b.ToTable("Adverts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Puikus butas. Arti mokykla.",
                            LoanPrice = 300.0,
                            OwnerId = 1,
                            RealEstateId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "",
                            LoanPrice = 220.0,
                            OwnerId = 2,
                            RealEstateId = 2
                        },
                        new
                        {
                            Id = 3,
                            Description = "Išnuomuojame ilgalaikei nuomai butą varpų 11-36, šalia prekybos centro, butas labai šiltas ir sauletas, rakinama laiptine, patogus susisiekimas aplink daug mokyklų, su baldais.",
                            LoanPrice = 230.0,
                            OwnerId = 3,
                            RealEstateId = 3
                        });
                });

            modelBuilder.Entity("TenancyPlatform.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Duration");

                    b.Property<int?>("LandlordId");

                    b.Property<double>("Price");

                    b.Property<int?>("RealEstateId");

                    b.Property<DateTime>("Start");

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("LandlordId");

                    b.HasIndex("RealEstateId");

                    b.HasIndex("TenantId");

                    b.ToTable("Contracts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Duration = 12,
                            LandlordId = 1,
                            Price = 300.0,
                            RealEstateId = 1,
                            Start = new DateTime(2019, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TenantId = 3
                        });
                });

            modelBuilder.Entity("TenancyPlatform.Models.Failure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContractId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsFixed");

                    b.Property<int?>("ReporterId");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("ReporterId");

                    b.ToTable("Failures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContractId = 1,
                            Description = "Sugedusi durų spyna",
                            IsFixed = false,
                            ReporterId = 3
                        });
                });

            modelBuilder.Entity("TenancyPlatform.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("ReceiverId");

                    b.Property<int?>("SenderId");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Sveiki, mane domina jūsų nuomojamas butas. Ar būtų galimybė apžiūrėti butą ateinančią savaitę?",
                            Date = new DateTime(2019, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            ReceiverId = 1,
                            SenderId = 3
                        },
                        new
                        {
                            Id = 2,
                            Content = "Laba diena. Susitikti galime trečiadienį.",
                            Date = new DateTime(2019, 1, 1, 8, 24, 13, 0, DateTimeKind.Unspecified),
                            ReceiverId = 3,
                            SenderId = 1
                        },
                        new
                        {
                            Id = 3,
                            Content = "Ačiū. Iki",
                            Date = new DateTime(2019, 1, 1, 11, 37, 48, 0, DateTimeKind.Unspecified),
                            ReceiverId = 1,
                            SenderId = 3
                        },
                        new
                        {
                            Id = 4,
                            Content = "Sveiki, mane domina jūsų nuomojamas butas. Ar būtų galimybė apžiūrėti butą ateinančią savaitę?",
                            Date = new DateTime(2019, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            ReceiverId = 2,
                            SenderId = 3
                        });
                });

            modelBuilder.Entity("TenancyPlatform.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContractId");

                    b.Property<int>("PaymentStatus");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("Payments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContractId = 1,
                            PaymentStatus = 1
                        },
                        new
                        {
                            Id = 2,
                            ContractId = 1,
                            PaymentStatus = 0
                        });
                });

            modelBuilder.Entity("TenancyPlatform.Models.RealEstate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Area");

                    b.Property<int>("BuildYear");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<int>("Floor");

                    b.Property<string>("HouseNr");

                    b.Property<int?>("OwnerId");

                    b.Property<int>("Rooms");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("RealEstates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Area = 80.0,
                            BuildYear = 2007,
                            City = "Kaunas",
                            Country = "Lithuania",
                            Floor = 1,
                            HouseNr = "7",
                            OwnerId = 1,
                            Rooms = 3,
                            Street = "Veiveriu g."
                        },
                        new
                        {
                            Id = 2,
                            Area = 45.0,
                            BuildYear = 1999,
                            City = "Kaunas",
                            Country = "Lithuania",
                            Floor = 6,
                            HouseNr = "56A",
                            OwnerId = 1,
                            Rooms = 1,
                            Street = "Taikos pr."
                        },
                        new
                        {
                            Id = 3,
                            Area = 60.0,
                            BuildYear = 2011,
                            City = "Vilnius",
                            Country = "Lithuania",
                            Floor = 2,
                            HouseNr = "3",
                            OwnerId = 2,
                            Rooms = 2,
                            Street = "Šimulionio g."
                        });
                });

            modelBuilder.Entity("TenancyPlatform.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<string>("Description");

                    b.Property<int?>("PaymentId");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 15.0,
                            Description = "Internetas už 2019 02 mėn.",
                            PaymentId = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 50.0,
                            Description = "Šildymas už 2019 02 mėn.",
                            PaymentId = 1
                        },
                        new
                        {
                            Id = 3,
                            Amount = 26.0,
                            Description = "Elektra už 2019 02 mėn.",
                            PaymentId = 1
                        },
                        new
                        {
                            Id = 4,
                            Amount = 123.15000000000001,
                            Description = "Komunaliniai už 2019 03 mėn.",
                            PaymentId = 2
                        });
                });

            modelBuilder.Entity("TenancyPlatform.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Antanas",
                            LastName = "Antanaitis",
                            Password = "antanas",
                            Role = "landlord",
                            UserName = "antanas@gmail.com"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Jonas",
                            LastName = "Jonaitis",
                            Password = "jonas",
                            Role = "landlord",
                            UserName = "jonas@gmail.com"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Dovydas",
                            LastName = "Dovydaitis",
                            Password = "dovydas",
                            Role = "tenant",
                            UserName = "dovydas@gmail.com"
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Julius",
                            LastName = "Julaitis",
                            Password = "julius",
                            Role = "tenant",
                            UserName = "julius@gmail.com"
                        });
                });

            modelBuilder.Entity("TenancyPlatform.Models.Advert", b =>
                {
                    b.HasOne("TenancyPlatform.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("TenancyPlatform.Models.RealEstate", "RealEstate")
                        .WithMany()
                        .HasForeignKey("RealEstateId");
                });

            modelBuilder.Entity("TenancyPlatform.Models.Contract", b =>
                {
                    b.HasOne("TenancyPlatform.Models.User", "Landlord")
                        .WithMany()
                        .HasForeignKey("LandlordId");

                    b.HasOne("TenancyPlatform.Models.RealEstate", "RealEstate")
                        .WithMany()
                        .HasForeignKey("RealEstateId");

                    b.HasOne("TenancyPlatform.Models.User", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("TenancyPlatform.Models.Failure", b =>
                {
                    b.HasOne("TenancyPlatform.Models.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractId");

                    b.HasOne("TenancyPlatform.Models.User", "Reporter")
                        .WithMany()
                        .HasForeignKey("ReporterId");
                });

            modelBuilder.Entity("TenancyPlatform.Models.Message", b =>
                {
                    b.HasOne("TenancyPlatform.Models.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId");

                    b.HasOne("TenancyPlatform.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("TenancyPlatform.Models.Payment", b =>
                {
                    b.HasOne("TenancyPlatform.Models.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractId");
                });

            modelBuilder.Entity("TenancyPlatform.Models.RealEstate", b =>
                {
                    b.HasOne("TenancyPlatform.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("TenancyPlatform.Models.Service", b =>
                {
                    b.HasOne("TenancyPlatform.Models.Payment", "Payment")
                        .WithMany("Services")
                        .HasForeignKey("PaymentId");
                });
#pragma warning restore 612, 618
        }
    }
}
