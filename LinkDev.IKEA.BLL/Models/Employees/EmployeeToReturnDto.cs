using LinkDev.IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
	public class EmployeeToReturnDto
	{
        //[Required]
        //[MaxLength(50, ErrorMessage = "max lenght of name is 50 chars")]
        //[MinLength(5, ErrorMessage = "min lenght of name is 5 chars")]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
		//[Range(22,30)]
		public int? Age { get; set; }


		[DataType(DataType.Currency)]
		public decimal Salary { get; set; }

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; }
		[EmailAddress]
		public string? Email { get; set; }


		public string Gender { get; set; } = null!;

		public string EmployeeType { get; set; } = null!;
	}
}
