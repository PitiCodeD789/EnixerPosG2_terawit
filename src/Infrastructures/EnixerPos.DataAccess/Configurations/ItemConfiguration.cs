using EnixerPos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Configurations
{
    public class ItemConfiguration : BaseConfiguration<ItemEntity>
    {
        public override void Configure(EntityTypeBuilder<ItemEntity> e)
        {
            base.Configure(e);
            e.ToTable("Items");
        }

    }
}
