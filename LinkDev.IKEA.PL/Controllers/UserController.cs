using AutoMapper;
using LinkDev.IKEA.DAL.Entites.Employees;
using LinkDev.IKEA.DAL.Entites.Identity;
using LinkDev.IKEA.DAL.Persistance.UnitOfWork;
using LinkDev.IKEA.PL.ViewModels;
using LinkDev.IKEA.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _UserManager;
		private readonly RoleManager<IdentityRole>_RoleManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
			_UserManager = userManager;
			_RoleManager = roleManager;
            _mapper = mapper;
        }

		public RoleManager<IdentityRole> RoleManager { get; }

		public async Task <IActionResult> Index(string email)

		{
			if (string.IsNullOrEmpty(email))
			{
				var users = await _UserManager.Users.Select(u => new UserViewModel
				{
					Id = u.Id,
					FName = u.FName,
					LName = u.LName,
					PhoneNumber = u.PhoneNumber,
					Email = u.Email,
					Roles = _UserManager.GetRolesAsync(u).Result
				}).ToListAsync();

				return View(users);
			}
			else
			{
				var user = await _UserManager.FindByEmailAsync(email);
				if (user != null)
				{
					var mappedUser = new UserViewModel
					{
						Id = user.Id,
						FName = user.FName,
						LName = user.LName,
						PhoneNumber = user.PhoneNumber,
						Email = user.Email,
						Roles = _UserManager.GetRolesAsync(user).Result
					};

					return View(new List<UserViewModel> { mappedUser });
				}
				else
				{
					return View(Enumerable.Empty<UserViewModel>());
				}
			}
			
			
		}

    

        [HttpGet]
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
            {
                return BadRequest(); // 400
            }

            var user = await _UserManager.FindByIdAsync(id);

            if (user is null)
            {
                return NotFound(); // 404
            }

            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);

            return View(viewName, mappedUser);
        }



		[HttpGet]

		public async Task<IActionResult> Edit(string id, string viewName = "Edit") {
            if (id is null)
            {
                return BadRequest(); // 400
            }

            var user = await _UserManager.FindByIdAsync(id);

            if (user is null)
            {
                return NotFound(); // 404
            }

            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);

            return View(viewName, mappedUser);
        }
		[HttpPost]
        public async Task< IActionResult> Edit([FromRoute] string id, UserViewModel UserVM)
        { 
            if (id != UserVM.Id)
            {
                return BadRequest();
            }
            //if (!ModelState.IsValid)
            //{
            //    return View(UserVM);
            //}

            try
            {
                var User = await _UserManager.FindByIdAsync(id);
                User.PhoneNumber = UserVM.PhoneNumber;
                User.FName = UserVM.FName;

                var result = await _UserManager.UpdateAsync(User);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
				// 1. Log Exception
				// 2. Friendly Msg

				return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id, string viewName = "Edit")
        {
            if (id is null)
            {
                return BadRequest(); // 400
            }

            var user = await _UserManager.FindByIdAsync(id);

            if (user is null)
            {
                return NotFound(); // 404
            }

            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);

            return View(viewName, mappedUser);
        }

[HttpPost]
        public async Task<IActionResult> Delete(UserViewModel UserVm)
        {
            try
            {
                var user = await _UserManager.FindByIdAsync(UserVm.Id);
                await _UserManager.DeleteAsync(user);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Msg
                return BadRequest();

            }
        }


    }
}
