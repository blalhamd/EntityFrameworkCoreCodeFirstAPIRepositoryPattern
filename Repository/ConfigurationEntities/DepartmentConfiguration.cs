﻿using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiddelLayer.ConfigurationEntities
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments").HasKey(t => t.Id);

            builder.Property(x => x.Title).HasColumnType("varchar").HasMaxLength(250).IsRequired();
            builder.Property(x => x.Description).HasColumnType("varchar").HasMaxLength(250).IsRequired();


        }
    }


}
