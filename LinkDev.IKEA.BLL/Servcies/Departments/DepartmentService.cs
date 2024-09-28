﻿using Link.Dev.IKEA.BLL.Models.Departments;
using Link.Dev.IKEA.DAL.Persistence.Repositories.Departments;
using LinkDev.IKEA.BLL.Servcies.Employees;
using LinkDev.IKEA.DAL.Entites.Departments;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
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
		private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork )
        {
			_unitOfWork = unitOfWork;
        }
        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
         {
            //var Departments = _departmentRepository.GetAll();
            //foreach (var department in Departments)
            //{
            //  yield  return new DepartmentToReturnDto()
            //    {
            //        Id = department.Id,
            //        Name = department.Name,
            //        Description=department.Description,
            //        CreationDate = department.CreationDate,
            //        Code    = department.Code,
            //    };
            //}

        
            //yield return (DepartmentToReturnDto)Departments;

            //yield return new DepartmentToReturnDto
            //{
            //    Id = department.Id,
            //    Code = department.Code,
            //    Name = department.Name,
            //    Description = department.Description,
            //    CreationDate = department.CreationDate,
            //};




            var Departments2 = _unitOfWork.DepartmentRepository.GetAllAsIQueryable().Where(d => !d.IsDeleted).Select(D => new DepartmentToReturnDto
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
            var Departments = _unitOfWork.DepartmentRepository.GetAllAsIQueryable().Select(D => new DepartmentToReturnDto
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
            var department = _unitOfWork.DepartmentRepository.GetById(id);
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
             _unitOfWork.DepartmentRepository.Add(department);
            return _unitOfWork.Complete();
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
             _unitOfWork.DepartmentRepository.Update(department);

            return _unitOfWork.Complete();
        }
        public bool DeleteDepartment(int id)
        {
            var DepartmentUnit = _unitOfWork.DepartmentRepository;

            var department = DepartmentUnit.GetById(id);
            if (department is not null)
            {
                var employess = _unitOfWork.EmployeeRepository.GetAllAsIQueryable().Where(e => e.Department == department);

                foreach (var employee  in employess)
                {
                    employee.DepartmentId = null;
                }
                 DepartmentUnit.Delete(department) ;

                return _unitOfWork.Complete() > 0;
            }
            return false;
        }
    }
}