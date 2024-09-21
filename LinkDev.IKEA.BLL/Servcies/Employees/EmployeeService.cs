using Azure;
using Link.Dev.IKEA.DAL.Data;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entites.Employees;
using LinkDev.IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Servcies.Employees
{
	public class EmployeeService : IEmployesService
	{
		private readonly IEmployeeRepository _employeeRepository;

		public EmployeeService(IEmployeeRepository employeeRepository)
        {
			_employeeRepository = employeeRepository;
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
				LastModifiedOn= DateTime.UtcNow

			};

			return _employeeRepository.Add(employee);

		}

		public bool DeleteEmploye(int id)
		{
			var employee = _employeeRepository.GetById(id);
if(employee is { })
			{
				return _employeeRepository.Delete(employee) > 0;
			}

return false;
		}

		public IEnumerable<EmployeeToReturnDto> GetAllEmployes()
		{
			return _employeeRepository.GetAllAsIQueryable().Where(e=>!e.IsDeleted).Select(emploee => new EmployeeToReturnDto
			{

				Id = emploee.Id,
				Name = emploee.Name,
				Age = emploee.Age,
				IsActive = emploee.IsActive,
				Salary = emploee.Salary,
				Email = emploee.Email,
			
				Gender = emploee.Gender.ToString(),
				EmployeeType = emploee.EmployeeType.ToString() ,
			
			}).ToList();

		}

		public EmployeeDetailsToReturnDto? GetEmployesById(int id)
		{
		var emploee = _employeeRepository.GetById(id); ;
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
				LastModifiedOn = DateTime.UtcNow

			};
			return _employeeRepository.Update(employee);

		}
	}
}
