using Link.Dev.IKEA.BLL.Models.Departments;
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

        public async Task< IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsynce()
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




            var Departments2 = await _unitOfWork.DepartmentRepository.GetAllAsIQueryable().Include(p=>p.Manger).Where(d => !d.IsDeleted).Select(D => new DepartmentToReturnDto
            {
                Id = D.Id,
                Code = D.Code,
                Name = D.Name,
                Description = D.Description,
                CreationDate = D.CreationDate,
                Manger = D.Manger.Name,
            }).AsNoTracking().ToListAsync();

            return  Departments2;

        }
        //public IEnumerable<DepartmentToReturnDto> GetAllDepartmentsIQueryable()
        //{
        //    var Departments = _unitOfWork.DepartmentRepository.GetAllAsIQueryable().Select(D => new DepartmentToReturnDto
        //    {
        //        Id = D.Id,
        //        Code = D.Code,
        //        Name = D.Name,
        //        Description = D.Description,
        //        CreationDate = D.CreationDate,
        //    }).AsNoTracking().ToList();
        //    return Departments;
        //}

        public async Task< DepartmentDetailsToReturnDto?> GetDepartmentsByIdAsynce(int id)

        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsynce(id);
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
                    LastModifiedOn = department.LastModifiedOn,
                    Manger = department.Manger.Name,
                    MangerId = department.MangerId,
                };
            return null;
        }

        //public async Task< int> CreateDepartmentAsynce(CreatedDepartmentDto DepartmentDto)

        //{
        //    var department = new Department
        //    {
        //        Code = DepartmentDto.Code,
        //        Name = DepartmentDto.Name,
        //        Description = DepartmentDto.Description,
        //        CreationDate = DepartmentDto.CreationDate,
        //        CreatedBy = 1,
        //        LastModifiedBy = 1,
        //        MangerId=DepartmentDto.MangerId
        //        //LastModifiedOn = DepartmentDto.LastModifiedOn,
        //    };
        //     _unitOfWork.DepartmentRepository.Add(department);
        //    return await _unitOfWork.CompleteAsynce();
        //}

        public async Task<int> CreateDepartmentAsynce(CreatedDepartmentDto departmentDto)
        {
            // Check if the MangerId already exists in the Departments table
            var existingDepartment = await _unitOfWork.DepartmentRepository
                .GetAllAsIQueryable()
                .AnyAsync(d => d.MangerId == departmentDto.MangerId && !d.IsDeleted);

            if (existingDepartment)
            {

                throw new InvalidOperationException("This manager is already assigned to another department.");
            }

            var department = new Department
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                LastModifiedBy = 1,
                MangerId = departmentDto.MangerId
            };

            _unitOfWork.DepartmentRepository.Add(department);
            return await _unitOfWork.CompleteAsynce();
        }



        //public async Task< int> UpdateDepartmentAsynce(UpdatedDepartmentDto DepartmentDto)

        //{
        //    var department = new Department
        //    {
        //        //Id = DepartmentDto.Id,
        //        Code = DepartmentDto.Code,
        //        Name = DepartmentDto.Name,
        //        Description = DepartmentDto.Description,
        //        CreationDate = DepartmentDto.CreationDate,
        //        MangerId=DepartmentDto.MangerId
        //        //CreatedBy = 1,
        //        //LastModifiedBy = 1,
        //        //LastModifiedOn = DepartmentDto.LastModifiedOn,



        //    };
        //     _unitOfWork.DepartmentRepository.Update(department);

        //    return await _unitOfWork.CompleteAsynce();
        //}

        public async Task<int> UpdateDepartmentAsynce(UpdatedDepartmentDto departmentDto)
        {
            // Check if the MangerId already exists in the Departments table
            var existingDepartment = await _unitOfWork.DepartmentRepository
                .GetAllAsIQueryable()
                .AnyAsync(d => d.MangerId == departmentDto.MangerId && d.Id != departmentDto.Id && !d.IsDeleted);

            if (existingDepartment)
            {
                throw new InvalidOperationException("This manager is already assigned to another department.");
            }

            var department = new Department
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                MangerId = departmentDto.MangerId
            };

            _unitOfWork.DepartmentRepository.Update(department);
            return await _unitOfWork.CompleteAsynce();
        }

        public async Task< bool> DeleteDepartmentAsynce(int id)

        {
            var DepartmentUnit = _unitOfWork.DepartmentRepository;

            var department = await DepartmentUnit.GetByIdAsynce(id);

            if (department is not null)
            {
                department.MangerId = null;

                var employess = _unitOfWork.EmployeeRepository.GetAllAsIQueryable().Where(e => e.DepartmentId == department.Id).ToList();

                foreach (var employee  in employess)
                {
                    employee.DepartmentId = null;
                }
                 DepartmentUnit.Delete(department) ;

                return await _unitOfWork.CompleteAsynce() > 0;
            }
            return false;
        }
      
    }
}