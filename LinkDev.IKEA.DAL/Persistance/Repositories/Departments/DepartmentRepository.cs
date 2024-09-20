using Link.Dev.IKEA.DAL.Data;
using LinkDev.IKEA.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Link.Dev.IKEA.DAL.Persistence.Repositories.Departments
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Department> GetAll(bool AsNoTraking = true)
        {
            if (AsNoTraking)
            {
                return _dbContext.Departments.AsNoTracking().ToList();
            }
            return _dbContext.Departments.ToList();
        }
        public Department? GetById(int id)
        {
            ///var department = _dbContext.Departments.Local.FirstOrDefault(d => d.Id == id);
            ///if(department is null)
            ///	department = _dbContext.Departments.FirstOrDefault(d => d.Id == id);
            ///return department;
            return _dbContext.Departments.Find(id);
        }
        public int Add(Department entity)
        {
            _dbContext.Departments.Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(Department entity)
        {
            _dbContext.Departments.Update(entity);
            return _dbContext.SaveChanges();
        }
        public int Delete(Department entity)
        {
            _dbContext.Departments.Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}