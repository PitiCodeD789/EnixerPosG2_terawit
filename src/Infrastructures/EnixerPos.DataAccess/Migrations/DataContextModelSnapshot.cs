﻿// <auto-generated />
using System;
using EnixerPos.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EnixerPos.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EnixerPos.Domain.Entities.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color");

                    b.Property<DateTime>("CreateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EnixerPos.Domain.Entities.DeviceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("PosName");

                    b.Property<int>("StoreId");

                    b.Property<DateTime>("UpdateDateTime");

                    b.HasKey("Id");

                    b.ToTable("Device");
                });

            modelBuilder.Entity("EnixerPos.Domain.Entities.DiscountEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("CreateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("DiscountName");

                    b.Property<bool>("IsPercentage");

                    b.Property<DateTime>("UpdateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("EnixerPos.Domain.Entities.ItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<string>("Color");

                    b.Property<decimal>("Cost");

                    b.Property<DateTime>("CreateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("Name");

                    b.Property<string>("Option1");

                    b.Property<decimal>("Option1Price");

                    b.Property<string>("Option2");

                    b.Property<decimal>("Option2Price");

                    b.Property<string>("Option3");

                    b.Property<decimal>("Option3Price");

                    b.Property<string>("Option4");

                    b.Property<decimal>("Option4Price");

                    b.Property<string>("Option5");

                    b.Property<decimal>("Option5Price");

                    b.Property<string>("Option6");

                    b.Property<decimal>("Option6Price");

                    b.Property<decimal>("Price");

                    b.Property<DateTime>("UpdateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("EnixerPos.Domain.Entities.ManageCashEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<int>("ManageCashStatus");

                    b.Property<int>("PosUserId");

                    b.Property<int>("ShiftId");

                    b.Property<string>("StoreEmail");

                    b.Property<DateTime>("UpdateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.HasKey("Id");

                    b.ToTable("ManageCash");
                });

            modelBuilder.Entity("EnixerPos.Domain.Entities.ReceiptEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<decimal>("Discount");

                    b.Property<bool>("IsDiscountPercentage");

                    b.Property<string>("ItemList");

                    b.Property<int>("PaymentType");

                    b.Property<string>("Reference");

                    b.Property<int>("ShiftId");

                    b.Property<string>("StoreEmail");

                    b.Property<decimal>("Total");

                    b.Property<decimal>("TotalDiscount");

                    b.Property<DateTime>("UpdateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.HasKey("Id");

                    b.ToTable("Receipt");
                });

            modelBuilder.Entity("EnixerPos.Domain.Entities.ShiftEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Available");

                    b.Property<decimal>("CashPayment");

                    b.Property<decimal>("CashRefunds");

                    b.Property<DateTime>("CreateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<decimal>("CreditCard");

                    b.Property<decimal>("DebitCard");

                    b.Property<decimal>("Discount");

                    b.Property<decimal>("Paidin");

                    b.Property<decimal>("Paidout");

                    b.Property<int>("PosUserId");

                    b.Property<decimal>("QRCode");

                    b.Property<decimal>("Refunds");

                    b.Property<int>("ShiftId");

                    b.Property<decimal>("StartingCash");

                    b.Property<string>("StoreEmail");

                    b.Property<DateTime>("UpdateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.HasKey("Id");

                    b.ToTable("Shift");
                });

            modelBuilder.Entity("EnixerPos.Domain.Entities.StoreEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("Salt");

                    b.Property<string>("StoreName");

                    b.Property<DateTime>("UpdateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.HasKey("Id");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("EnixerPos.Domain.Entities.TokenEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("Email");

                    b.Property<string>("RefreshToken");

                    b.Property<DateTime>("UpdateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Token");
                });

            modelBuilder.Entity("EnixerPos.Domain.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.Property<string>("Email");

                    b.Property<string>("NameUser");

                    b.Property<string>("Pin");

                    b.Property<string>("Salt");

                    b.Property<DateTime>("UpdateDateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GetUtcDate()");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
