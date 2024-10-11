using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Link.Dev.IKEA.BLL.Models.Departments
{
    public class CreatedDepartmentDto
    {
        //public int CreatedBy { get; set; }
        //public DateTime CreateOn { get; set; }
        //public int LastModifiedBy { get; set; }
        //public DateTime LastModifiedOn { get; set; }
        [Required(ErrorMessage = "Code is Requered")]

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateOnly CreationDate { get; set; }

        [Display(Name ="Manger Name")]
        public int MangerId { get; set; }

    }
}