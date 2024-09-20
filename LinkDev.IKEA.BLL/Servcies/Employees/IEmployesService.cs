using Link.Dev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Servcies.Employees
{
	public interface IEmployesService
	{
		IEnumerable<EmployeeToReturnDto> GetAllEmployes();

	EmployeeDetailsToReturnDto? GetEmployesById(int id);

		int CreateEmploye(CreatedEmployeeDto departmentDto);

		int UpdateEmploye(UpdatedEmployeeDto DepartmentDto);

		bool DeleteEmploye(int id);
	}
}
