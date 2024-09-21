using Link.Dev.IKEA.BLL.Models.Departments;
using Link.Dev.IKEA.DAL.Persistence.Repositories.Departments;
using LinkDev.IKEA.DAL.Entites.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Link.Dev.IKEA.BLL.Services.Departments
{
    public class DepartmentService : IDepratmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
         {
            //var Departments = _departmentRepository.GetAll();
            //foreach (var department in Departments)
            //{
            //    /// yield return new DepartmentToReturnDto
            //    /// {
            //    /// 	Id = department.Id,
            //    /// 	Code = department.Code,
            //    /// 	Name = department.Name,
            //    /// 	Description = department.Description,
            //    /// 	CreationDate = department.CreationDate,
            //    /// };
            //    //yield return (DepartmentToReturnDto)Departments;
            //    //yield return new DepartmentToReturnDto
            //    //{
            //    //    Id = department.Id,
            //    //    Code = department.Code,
            //    //    Name = department.Name,
            //    //    Description = department.Description,
            //    //    CreationDate = department.CreationDate,
            //    //};


            //}

            var Departments2 = _departmentRepository.GetAllAsIQueryable().Where(d=> !d.IsDeleted).Select(D => new DepartmentToReturnDto
            {
                Id = D.Id,
                Code = D.Code,
                Name = D.Name,
                Description = D.Description,
                CreationDate = D.CreationDate,
            }).AsNoTracking().ToList();
            return Departments2;
        }
        public IEnumerable<DepartmentToReturnDto> GetAllDepartmentsIQueryable()
        {
            var Departments = _departmentRepository.GetAllAsIQueryable().Select(D => new DepartmentToReturnDto
            {
                Id = D.Id,
                Code = D.Code,
                Name = D.Name,
                Description = D.Description,
                CreationDate = D.CreationDate,
            }).AsNoTracking().ToList();
            return Departments;
        }
        public DepartmentDetailsToReturnDto? GetDepartmentsById(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is not null)
                return new DepartmentDetailsToReturnDto
                {
                    Id = department.Id,
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    CreateOn = department.CreatedOn,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn
                };
            return null;
        }
        public int CreateDepartment(CreatedDepartmentDto DepartmentDto)
        {
            var department = new Department
            {
                Code = DepartmentDto.Code,
                Name = DepartmentDto.Name,
                Description = DepartmentDto.Description,
                CreationDate = DepartmentDto.CreationDate,
                CreatedBy = 1,
                LastModifiedBy = 1,
                //LastModifiedOn = DepartmentDto.LastModifiedOn,
            };
            return _departmentRepository.Add(department);
        }
        public int UpdateDepartment(UpdatedDepartmentDto DepartmentDto)
        {
            var department = new Department
            {
                Id = DepartmentDto.Id,
                Code = DepartmentDto.Code,
                Name = DepartmentDto.Name,
                Description = DepartmentDto.Description,
                CreationDate = DepartmentDto.CreationDate,
                //CreatedBy = 1,
                //LastModifiedBy = 1,
                //LastModifiedOn = DepartmentDto.LastModifiedOn,



            };
            return _departmentRepository.Update(department);

        }
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is not null)
            {
                return _departmentRepository.Delete(department) > 0;
            }
            return false;
        }
    }
}