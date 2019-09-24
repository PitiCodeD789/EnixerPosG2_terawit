using EnixerPos.DataAccess.Configurations;
using EnixerPos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Contexts
{
     public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=git.dookdik.me;Database=EnixerPosG2TestAuth;Trusted_Connection=True;user id=sa;password=Gg123456789;Integrated Security=false;");
            builder.EnableSensitiveDataLogging();
        }

        public DbSet<ManageCashEntity> ManageCash { get; set; }
         public DbSet<ReceiptEntity> Receipt { get; set; }
        public DbSet<ShiftEntity> Shift { get; set; }
        public DbSet<StoreEntity> Store { get; set; }
        public DbSet<TokenEntity> Token { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<DiscountEntity> Discounts { get; set; }
        public DbSet<ItemEntity> Items { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new ManageCashConfiguration());
            modelBuilder.ApplyConfiguration(new ReceiptConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new TokenConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DiscountConfiguration());

        }


    }

  
}
