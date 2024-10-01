using Azure;
using Link.Dev.IKEA.DAL.Data;
using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entites.Employees;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using LinkDev.IKEA.BLL.Common.Services.Attachments;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Servcies.Employees
{
	public class EmployeeService : IEmployesService
	{
		private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;


        public EmployeeService(IUnitOfWork unitOfWork, IAttachmentService attachmentService)
        {
			_unitOfWork = unitOfWork;
            _attachmentService = attachmentService;

        }
        public async Task<int> CreateEmployeAsynce(CreatedEmployeeDto employeeDto)
        {

            var employee = new Employee()
			{
				Name = employeeDto.Name,
				Age = employeeDto.Age,
				Adress = employeeDto.Adress,
				IsActive = employeeDto.IsActive,
				Salary = employeeDto.Salary,
				Email = employeeDto.Email,
				PhoneNumber = employeeDto.PhoneNumber,
				HiringDate = employeeDto.HiringDate,
				Gender = employeeDto.Gender,
				EmployeeType = employeeDto.EmployeeType,
				CreatedBy = 1,
				LastModifiedBy = 1,
				LastModifiedOn= DateTime.UtcNow,
				DepartmentId= employeeDto.DepartmentId,

			};

            if (employeeDto.Image is not null)
            {
                employee.Image = await _attachmentService.UploadAsynce(employeeDto.Image, "images");
            }

            _unitOfWork.EmployeeRepository.Add(employee);
            return await _unitOfWork.CompleteAsynce();
        }

        public async Task<bool> DeleteEmployeAsynce(int id)
        {
            var employeeUnit = _unitOfWork.EmployeeRepository;

            var employee = await employeeUnit.GetByIdAsynce(id);
            if (employee is { })
			{
				 employeeUnit.Delete(employee) ;
                return await _unitOfWork.CompleteAsynce() > 0;
            }

            return false;
		}

        public async Task<IEnumerable<EmployeeToReturnDto>> GetEmployesAsynce(string search)
        {

            return await _unitOfWork.EmployeeRepository
                .GetAllAsIQueryable()
				.Where(e=>!e.IsDeleted && (string.IsNullOrEmpty(search) || e.Name.ToLower().Contains(search.ToLower())))
				.Include(e => e.Department)
				.Select(emploee => new EmployeeToReturnDto
			{

				Id = emploee.Id,
				Name = emploee.Name,
				Age = emploee.Age,
				IsActive = emploee.IsActive,
				Salary = emploee.Salary,
				Email = emploee.Email,
			
				Gender = emploee.Gender.ToString(),
				EmployeeType = emploee.EmployeeType.ToString() ,
				Department=emploee.Department.Name,
				Image=	emploee.Image,

			}).ToListAsync();
			//var employee = result.ToList();
			//var emploee2 = result.FirstOrDefault();
			//return employee;
			//return emploee2;
		}

        public async Task<EmployeeDetailsToReturnDto?> GetEmployesByIdAsynce(int id)
        {
            var emploee = await _unitOfWork.EmployeeRepository.GetByIdAsynce(id);

            if (emploee is { })
			{
				return new EmployeeDetailsToReturnDto(){
					Id=emploee.Id,
					Name = emploee.Name,
				Age = emploee.Age,
				Adress = emploee.Adress,
				IsActive = emploee.IsActive,
				Salary = emploee.Salary,
				Email = emploee.Email,
				PhoneNumber = emploee.PhoneNumber,
				HiringDate = emploee.HiringDate,
				Gender = emploee.Gender,
				EmployeeType =emploee.EmployeeType,
					Department=emploee.Department?.Name,
					Image=	emploee.Image,
					
				};
			}
			else
			{
				return null;
			}
		}

        public async Task<int> UpdateEmployeAsynce(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
			{
				Id= employeeDto.Id,
				Name = employeeDto.Name,
				Age = employeeDto.Age,
				Adress = employeeDto.Adress,
				IsActive = employeeDto.IsActive,
				Salary = employeeDto.Salary,
				Email = employeeDto.Email,
				PhoneNumber = employeeDto.PhoneNumber,
				HiringDate = employeeDto.HiringDate,
				Gender = employeeDto.Gender,
				EmployeeType = employeeDto.EmployeeType,
				CreatedBy = 1,
				LastModifiedBy = 1,
				LastModifiedOn = DateTime.UtcNow,
				DepartmentId= employeeDto.DepartmentId,

			};
			if (employeeDto.Image is not null)
			{
				employee.Image = await _attachmentService.UploadAsynce(employeeDto.Image, "images");
			}
			_unitOfWork.EmployeeRepository.Update(employee);

            return await _unitOfWork.CompleteAsynce();
        }
    }
}
