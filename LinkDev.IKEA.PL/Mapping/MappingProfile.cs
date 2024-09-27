using AutoMapper;
using Link.Dev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.PL.ViewModels;

namespace LinkDev.IKEA.PL.Mapping
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {

            #region Employees



            #endregion

            #region Departments

            CreateMap<DepartmentDetailsToReturnDto, DepartmentViewModel>()
                ;

            CreateMap<DepartmentViewModel, UpdatedDepartmentDto>();

			CreateMap<DepartmentViewModel, CreatedDepartmentDto>();

			#endregion

		}


	}
}
