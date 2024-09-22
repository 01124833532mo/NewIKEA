
using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels
{
    public class DepartmentViewModel
    {

        //public int Id { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime CreateOn { get; set; }
        //public int LastModifiedBy { get; set; }
        //public DateTime LastModifiedOn { get; set; }
        [Required(ErrorMessage = "Code is Requered")]

        public string Code { get; set; } = null!;



        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name = "Creation of Date")]
        public DateOnly CreationDate { get; set; }
    }
}
