using EnixerPos.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Configurations
{
    class StoreConfiguration : BaseConfiguration<StoreEntity>
    {
        public override void Configure(EntityTypeBuilder<StoreEntity> e)
        {
            base.Configure(e);
        }
    }
}
