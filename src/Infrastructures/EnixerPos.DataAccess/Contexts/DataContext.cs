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
            builder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=EnixerPosG2;Trusted_Connection=True");
         
        }

    }
}
