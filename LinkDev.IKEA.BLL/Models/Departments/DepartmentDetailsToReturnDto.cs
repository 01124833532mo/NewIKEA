using LinkDev.IKEA.DAL.Entites.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Link.Dev.IKEA.BLL.Models.Departments
{
    public class DepartmentDetailsToReturnDto
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreateOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }

        public  string Manger { get; set; } = null!;

		public int? MangerId { get; set; }


	}
}