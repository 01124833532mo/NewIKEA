using LinkDev.IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
	public class EmployeeDetailsToReturnDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		//[Range(22,30)]
		public int? Age { get; set; }
		public string? Adress { get; set; }


		[DataType(DataType.Currency)]
		public decimal Salary { get; set; }

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; }
		[EmailAddress]
		public string? Email { get; set; }

		public string? PhoneNumber { get; set; }
		[Display(Name = "Hiring Date")]

		public DateOnly HiringDate { get; set; }

		
		public string Gender { get; set; } = null!;

		public string EmployeeType { get; set; } = null!;
	}
}
