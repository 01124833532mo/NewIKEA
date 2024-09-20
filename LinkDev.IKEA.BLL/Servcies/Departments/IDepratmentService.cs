using Link.Dev.IKEA.BLL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Dev.IKEA.BLL.Services.Departments
{
    public interface IDepratmentService
    {
        IEnumerable<DepartmentToReturnDto> GetAllDepartments();

        DepartmentDetailsToReturnDto? GetDepartmentsById(int id);

        int CreateDepartment(CreatedDepartmentDto departmentDto);

        int UpdateDepartment(UpdatedDepartmentDto DepartmentDto);

        bool DeleteDepartment(int id);
    }
}