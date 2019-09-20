using EnixerPos.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Configurations
{
    class ReceiptConfiguration : BaseConfiguration<ReceiptEntity>
    {
        public override void Configure(EntityTypeBuilder<ReceiptEntity> e)
        {
            base.Configure(e);
        }
    }
}
