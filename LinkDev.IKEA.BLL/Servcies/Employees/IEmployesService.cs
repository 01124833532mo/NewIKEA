using Link.Dev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Servcies.Employees
{
	internal interface IEmployesService
	{
		IEnumerable<EmployeeToReturnDto> GetAllDepartments();

	EmployeeDetailsToReturnDto? GetDepartmentsById(int id);

		int CreateDepartment(CreatedEmployeeDto departmentDto);

		int UpdateDepartment(UpdatedEmployeeDto DepartmentDto);

		bool DeleteDepartment(int id);
	}
}
