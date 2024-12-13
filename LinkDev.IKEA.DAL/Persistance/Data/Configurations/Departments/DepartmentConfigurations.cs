﻿using LinkDev.IKEA.DAL.Entites.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LinkDev.IKEA.DAL.Persistance.Data.Configurations.Departments
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);
            builder.Property(d => d.Code).HasColumnType("varchar(20)").IsRequired();
            builder.Property(d => d.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
            //builder.Property(d => d.CreatedBy).HasDefaultValueSql("");
            builder.Property(d => d.LastModifiedOn).HasDefaultValueSql("GETDATE()");
            //builder.Property(d => d.LastModifiedBy).HasDefaultValueSql("");

            builder.HasMany(p => p.Employess)
                .WithOne(p => p.Department)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.Property(p => p.MangerId).IsRequired(false);

            //builder.HasOne(p => p.Manger).WithOne(p => p.DepartmentManger)
            //    .HasForeignKey<Department>(p => p.MangerId).OnDelete(DeleteBehavior.SetNull);

    //        builder.HasOne(p => p.Manger)
    //.WithOne(p => p.DepartmentManger)
    //.HasForeignKey<Department>(p => p.MangerId)
    //.IsRequired()
    //.OnDelete(DeleteBehavior.Restrict);  // Use Restrict or Cascade depending on your business logic

        }
    }
}