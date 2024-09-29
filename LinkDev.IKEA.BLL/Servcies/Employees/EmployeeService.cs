using Azure;
using Link.Dev.IKEA.DAL.Data;
using LinkDev.IKEA.BLL.Common.Services.Attachments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entites.Employees;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
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

		public EmployeeService(IUnitOfWork unitOfWork , IAttachmentService attachmentService)
        {
			_unitOfWork = unitOfWork;
			_attachmentService = attachmentService;
		}
        public int CreateEmploye(CreatedEmployeeDto employeeDto)
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

			if(employeeDto.Image is not null)
			{
				employee.Image = _attachmentService.Upload(employeeDto.Image, "images");
			}

			 _unitOfWork.EmployeeRepository.Add(employee);
		return	_unitOfWork.Complete();
		}

		public bool DeleteEmploye(int id)
		{
			var employeeUnit = _unitOfWork.EmployeeRepository;

			var employee = employeeUnit.GetById(id);
if(employee is { })
			{
				 employeeUnit.Delete(employee) ;
				return _unitOfWork.Complete()>0;
			}

return false;
		}

		public IEnumerable<EmployeeToReturnDto> GetEmployes(string search)
		{

			return _unitOfWork.EmployeeRepository
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
				Image=emploee.Image,
			}).ToList();
			//var employee = result.ToList();
			//var emploee2 = result.FirstOrDefault();
			//return employee;
			//return emploee2;
		}

		public EmployeeDetailsToReturnDto? GetEmployesById(int id)
		{
		var emploee = _unitOfWork.EmployeeRepository.GetById(id); 
			if(emploee is { })
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

                                    Image = emploee.Image,

                };
			}
			else
			{
				return null;
			}
		}

		public int UpdateEmploye(UpdatedEmployeeDto employeeDto)
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
			 _unitOfWork.EmployeeRepository.Update(employee);

			return _unitOfWork.Complete();
		}
	}
}
