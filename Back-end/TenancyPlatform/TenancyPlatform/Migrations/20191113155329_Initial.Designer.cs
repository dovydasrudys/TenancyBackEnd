﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TenancyPlatform.Contexts;

namespace TenancyPlatform.Migrations
{
    [DbContext(typeof(TenancyContext))]
    [Migration("20191113155329_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
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
                });

            modelBuilder.Entity("TenancyPlatform.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Duration");

                    b.Property<int?>("LandlordId");

                    b.Property<double>("Price");

                    b.Property<DateTime>("Start");

                    b.Property<int?>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("LandlordId");

                    b.HasIndex("TenantId");

                    b.ToTable("Contracts");
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
                });

            modelBuilder.Entity("TenancyPlatform.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BeneficiaryId");

                    b.Property<int?>("ContractId");

                    b.Property<int?>("PayerId");

                    b.Property<int>("PaymentStatus");

                    b.HasKey("Id");

                    b.HasIndex("BeneficiaryId");

                    b.HasIndex("ContractId");

                    b.HasIndex("PayerId");

                    b.ToTable("Payments");
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

                    b.Property<int>("Rooms");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.ToTable("RealEstates");
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
                });

            modelBuilder.Entity("TenancyPlatform.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
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
                    b.HasOne("TenancyPlatform.Models.User", "Beneficiary")
                        .WithMany()
                        .HasForeignKey("BeneficiaryId");

                    b.HasOne("TenancyPlatform.Models.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("ContractId");

                    b.HasOne("TenancyPlatform.Models.User", "Payer")
                        .WithMany()
                        .HasForeignKey("PayerId");
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