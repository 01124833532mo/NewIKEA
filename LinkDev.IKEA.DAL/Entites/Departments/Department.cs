using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Link.Dev.IKEA.DAL.Entites;

namespace LinkDev.IKEA.DAL.Entites.Departments
{
    public class Department : ModelBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}