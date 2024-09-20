using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entites.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistance.Data.Configurations.Employees
{
	internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.Property(e => e.Name).HasColumnType("varchar(50)").IsRequired();
			builder.Property(e => e.Adress).HasColumnType("varchar(100)");

			builder.Property(e => e.Salary).HasColumnType("decimal(8,2)");

			builder.Property(e => e.Gender).HasConversion(
				(gender) => gender.ToString(),
				(gender) => (Gender)Enum.Parse(typeof(Gender), gender)

				);


			builder.Property(e => e.EmployeeType).HasConversion(
				(emploeetype) => emploeetype.ToString(),
				(emploeetype) => (EmployeeType)Enum.Parse(typeof(EmployeeType), emploeetype)

				);
			builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETUTCDATE()");

		}
	}
}
