using EnixerPos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Configurations
{
    public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> e)
        {
            e.HasKey(p => p.Id);

            e.Property(p => p.CreateDateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GetUtcDate()");

            e.Property(p => p.UpdateDateTime)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GetUtcDate()");
        }
    }
}
