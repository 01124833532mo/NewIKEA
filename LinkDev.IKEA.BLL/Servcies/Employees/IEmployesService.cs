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

		Task< IEnumerable<EmployeeToReturnDto>> GetEmployesAsynce(string search);

	 Task <EmployeeDetailsToReturnDto?> GetEmployesByIdAsynce(int id);

		Task <int> CreateEmployeAsynce(CreatedEmployeeDto departmentDto);

	Task	<int> UpdateEmployeAsynce(UpdatedEmployeeDto DepartmentDto);

	Task	<bool> DeleteEmployeAsynce(int id);
	}
}
