using EnixerPos.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Configurations
{
    class ShiftConfiguration : BaseConfiguration<ShiftEntity>
    {
        public override void Configure(EntityTypeBuilder<ShiftEntity> e)
        {
            base.Configure(e);
        }
    }
}
