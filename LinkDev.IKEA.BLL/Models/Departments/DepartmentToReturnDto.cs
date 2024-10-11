using LinkDev.IKEA.DAL.Entites.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Link.Dev.IKEA.BLL.Models.Departments
{
    public class DepartmentToReturnDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Display(Name = "Date Of Creation")]
        public DateOnly CreationDate { get; set; }

        public string Manger { get; set; } = null!;


        //public static explicit operator DepartmentToReturnDto(Department department)
        //{
        //    return new DepartmentToReturnDto
        //    {
        //        Id = department.Id,
        //        Code = department.Code,
        //        Name = department.Name,
        //        Description = department.Description,
        //        CreationDate = department.CreationDate,
        //    };
        //}
    }
}