using EnixerPos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Configurations
{
    public class DiscountConfiguration : BaseConfiguration<DiscountEntity>
    {
        public override void Configure(EntityTypeBuilder<DiscountEntity> e)
        {
            base.Configure(e);
            e.ToTable("Discounts");
        }

    }
}
