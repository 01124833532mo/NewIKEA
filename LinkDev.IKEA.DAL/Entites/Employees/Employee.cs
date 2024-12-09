using Link.Dev.IKEA.DAL.Entites;
using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entites.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Entites.Employees
{
	public class Employee : ModelBase
	{
		//[Required]
		//[MaxLength(50, ErrorMessage = "max lenght of name is 50 chars")]
		//[MinLength(5, ErrorMessage = "min lenght of name is 5 chars")]

		public string Name { get; set; } = null!;
		//[Range(22,30)]
        public int? Age { get; set; }

		//[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",ErrorMessage ="Adress must like 123-streate-city-country")]
        public string Adress { get; set; }
		//[DataType(DataType.Currency)]
		public decimal Salary { get; set; }

		//[Display(Name ="Is Active")]
        public bool IsActive { get; set; }
		//[EmailAddress]
        public string? Email { get; set; }
		//[Display(Name ="Phone Number")]
		//[Phone]
        public string? PhoneNumber { get; set; }
		//[Display(Name = "Hiring Date")]

		public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }
		 
        public EmployeeType EmployeeType { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department? DepartmentManger { get; set; }

        public virtual Department? Department { get; set; }

        public string? Image { get; set; }

    }
}
