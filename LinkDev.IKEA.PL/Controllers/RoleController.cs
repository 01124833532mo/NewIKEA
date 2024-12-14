using AutoMapper;
using LinkDev.IKEA.DAL.Entites.Identity;
using LinkDev.IKEA.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.PL.Controllers
{
	public class RoleController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IMapper _mapper;

		public RoleController(RoleManager<IdentityRole> roleManager , IMapper mapper)
        {
			_roleManager = roleManager;
			_mapper = mapper;
		}
		public async Task<IActionResult> Index(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				var roles = await _roleManager.Roles.Select(u => new RoleViewModel
				{
					Id = u.Id,
					RoleName =u.Name
				}).ToListAsync();

				return View(roles);
			}
			else
			{
				var role = await _roleManager.FindByNameAsync(name);
				if (role != null)
				{
					var mappedUser = new RoleViewModel
					{
						Id= role.Id,
						RoleName = role.Name
					};

					return View(new List<RoleViewModel> { mappedUser });
				}
				else
				{
					return View(Enumerable.Empty<RoleViewModel>());
				}
			}


		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(RoleViewModel RoleVM)
		{
			if (ModelState.IsValid)
			{
				var mappedRole = _mapper.Map<RoleViewModel, IdentityRole>(RoleVM);
				await _roleManager.CreateAsync(mappedRole);

				return RedirectToAction(nameof(Index));


			}
			return View(RoleVM);

		}


		[HttpGet]
		public async Task<IActionResult> Details(string id, string viewName = "Details")
		{
			if (id is null)
			{
				return BadRequest(); // 400
			}

			var role = await _roleManager.FindByIdAsync(id);

			if (role is null)
			{
				return NotFound(); // 404
			}

			var mappedUser = _mapper.Map<IdentityRole, RoleViewModel>(role);

			return View(viewName, mappedUser);
		}



		[HttpGet]

		public async Task<IActionResult> Edit(string id, string viewName = "Edit")
		{

			if (id is null)
			{
				return BadRequest(); // 400
			}

			var role = await _roleManager.FindByIdAsync(id);

			if (role is null)
			{
				return NotFound(); // 404
			}

			var mappedUser = _mapper.Map<IdentityRole, RoleViewModel>(role);

			return View(viewName, mappedUser);
		}
		[HttpPost]
		public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel RoleVm)
		{
			if (id != RoleVm.Id)
			{
				return BadRequest();
			}
			//if (!ModelState.IsValid)
			//{
			//    return View(UserVM);
			//}

			try
			{
				var Role = await _roleManager.FindByIdAsync(id);
				Role.Name = RoleVm.RoleName;

				var result = await _roleManager.UpdateAsync(Role);

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

			var role = await _roleManager.FindByIdAsync(id);

			if (role is null)
			{
				return NotFound(); // 404
			}

			var mappedUser = _mapper.Map<IdentityRole, RoleViewModel>(role);

			return View(viewName, mappedUser);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(UserViewModel UserVm)
		{
			try
			{
				var role = await _roleManager.FindByIdAsync(UserVm.Id);
				await _roleManager.DeleteAsync(role);

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
