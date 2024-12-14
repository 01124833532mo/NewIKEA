using AutoMapper;
using Link.Dev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.DAL.Entites.Identity;
using LinkDev.IKEA.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.IKEA.PL.Mapping
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {

            #region Employees



            #endregion

            #region Departments

            CreateMap<DepartmentDetailsToReturnDto, DepartmentViewModel>()  ;

            CreateMap<DepartmentViewModel, UpdatedDepartmentDto>();

			CreateMap<DepartmentViewModel, CreatedDepartmentDto>();
            CreateMap<ApplicationUser,UserViewModel>().ReverseMap() ;
            CreateMap<RoleViewModel, IdentityRole>().ForMember(d=>d.Name,o=>o.MapFrom(s=>s.RoleName)).ReverseMap() ;
			#endregion

		}


	}
}
