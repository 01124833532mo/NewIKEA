using Link.Dev.IKEA.DAL.Data;
using LinkDev.IKEA.DAL.Entites.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Link.Dev.IKEA.DAL.Persistence.Repositories.Departments
{
    public class DepartmentRepository :GenericRepository<Department> ,IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
	}
}