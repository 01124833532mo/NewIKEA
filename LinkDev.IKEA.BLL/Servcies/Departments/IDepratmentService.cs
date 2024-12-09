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

        Task <IEnumerable<DepartmentToReturnDto>> GetAllDepartmentsAsynce();

       Task <DepartmentDetailsToReturnDto?> GetDepartmentsByIdAsynce(int id);

       Task <int> CreateDepartmentAsynce(CreatedDepartmentDto departmentDto);

        Task <int> UpdateDepartmentAsynce(UpdatedDepartmentDto DepartmentDto);

       Task <bool> DeleteDepartmentAsynce(int id);


    }
}