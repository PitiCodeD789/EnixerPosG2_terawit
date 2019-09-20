using EnixerPos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Configurations
{
    class ManageCashConfiguration :  BaseConfiguration<ManageCashEntity>
    {
        public override void Configure(EntityTypeBuilder<ManageCashEntity> e)
        {
            base.Configure(e);
            e.ToTable("ManageCash");

        }
    }
}
