using EnixerPos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Configurations
{
    public class CategoryConfiguration : BaseConfiguration<CategoryEntity>
    {
        public override void Configure(EntityTypeBuilder<CategoryEntity> e)
        {
            base.Configure(e);
            e.ToTable("Categories");
        }

    }
}
