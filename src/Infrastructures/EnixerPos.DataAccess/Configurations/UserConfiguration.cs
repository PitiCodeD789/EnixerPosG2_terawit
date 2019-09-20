using EnixerPos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Configurations
{
    class UserConfiguration : BaseConfiguration<UserEntity>
    {

        public override void Configure(EntityTypeBuilder<UserEntity> e)
        {
            base.Configure(e);
            e.ToTable("User");
          

        }


    }
}
