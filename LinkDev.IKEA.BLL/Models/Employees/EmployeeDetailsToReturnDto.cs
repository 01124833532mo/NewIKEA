﻿using LinkDev.IKEA.DAL.Common.Enums;
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

		
		public Gender Gender { get; set; } 

		public EmployeeType EmployeeType { get; set; } 

        public DateTime CreateOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public DateOnly CreationDate { get; set; }
        public int CreatedBy { get; set; }


        public string? Department { get; set; }

        public int? DepartmentId { get; set; }

  

    
		public string? Image { get; set; }

	}
}
