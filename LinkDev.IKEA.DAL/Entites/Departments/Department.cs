using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Link.Dev.IKEA.DAL.Entites;
using LinkDev.IKEA.DAL.Entites.Employees;

namespace LinkDev.IKEA.DAL.Entites.Departments
{
    public class Department : ModelBase
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public DateOnly CreationDate { get; set; }


        public virtual ICollection<Employee> Employess { get; set; }=new HashSet<Employee>();   
    }
}