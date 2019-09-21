using EnixerPos.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Configurations
{
    class TokenConfiguration : BaseConfiguration<TokenEntity>
    {



        public override void Configure(EntityTypeBuilder<TokenEntity> e)
        {
            base.Configure(e);
          
        }


    }
}
