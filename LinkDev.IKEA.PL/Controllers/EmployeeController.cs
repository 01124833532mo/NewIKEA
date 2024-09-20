using Link.Dev.IKEA.BLL.Models.Departments;
using Link.Dev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Servcies.Employees;
using LinkDev.IKEA.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IEmployesService _employesService;
		private readonly ILogger _logger;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public EmployeeController(IEmployesService employesService, ILogger<DepartmentController> logger, IWebHostEnvironment webHostEnvironment)
		{
			_employesService = employesService;
			_logger = logger;
			_webHostEnvironment = webHostEnvironment;
		}

		[HttpGet]

		public IActionResult Index()
		{
			var employees = _employesService.GetAllEmployes();
			return View(employees);
		}

		[HttpGet]

		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(CreatedEmployeeDto employeeDto)
		{
			if (!ModelState.IsValid)
				return View(employeeDto);
			var message = string.Empty;
			try
			{
				var result = _employesService.CreateEmploye(employeeDto);
				if (result > 0)
					return RedirectToAction(nameof(Index));
				else
				{
					message = "Employee is not Created";
					ModelState.AddModelError(string.Empty, message);
					return View(result);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				if (_webHostEnvironment.IsDevelopment())
				{
					message = ex.Message;
					return View(employeeDto);
				}
				else
				{
					message = "Employee is not Created";
					return View("Error", message);
				}
			}
		}

		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			var employee = _employesService.GetEmployesById(id.Value);
			if (employee == null)
			{
				return NotFound();
			}
			return View(employee);
		}

		//[HttpGet]
		//public IActionResult Edit(int? id)
		//{

		//	if (id is null)
		//	{
		//		return BadRequest();
		//	}

		//	var employee = _employesService.GetEmployesById(id.Value);

		//	// Check if department exists
		//	if (employee == null)
		//	{
		//		return NotFound();  // Return 404 if the department is not found
		//	}

		//	// Pass the fetched department data to the view
		//	return View(new UpdatedDepartmentViewModel
		//	{
		//		Code = department.Code,
		//		//Id = department.Id,
		//		Name = department.Name,
		//		CreationDate = department.CreationDate,
		//		Description = department.Description,




		//	});
		//}
		//[HttpPost]
		//public IActionResult Edit([FromRoute] int id, UpdatedDepartmentViewModel departmentVm)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return View(departmentVm);
		//	}
		//	var message = string.Empty;
		//	try
		//	{
		//		var departmenttoUpdate = new UpdatedDepartmentDto
		//		{
		//			Code = departmentVm.Code,
		//			Id = id,
		//			Name = departmentVm.Name,
		//			CreationDate = departmentVm.CreationDate,
		//			Description = departmentVm.Description

		//		};
		//		var updated = _employesService.UpdateDepartment(departmenttoUpdate) > 0;
		//		if (updated)
		//		{
		//			return RedirectToAction(nameof(Index));
		//		}

		//		message = "an error has occured during updating the department";
		//	}
		//	catch (Exception ex)
		//	{

		//		_logger.LogError(ex, ex.Message);

		//		message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during updating the department";

		//	}
		//	ModelState.AddModelError(string.Empty, message);
		//	return View(departmentVm);
		//}
		//[HttpGet]
		//public IActionResult Delete(int? id)
		//{
		//    if (id == null)
		//    {
		//        return BadRequest();
		//    }
		//    var department = _depratmentService.GetDepartmentsById(id.Value);
		//    if (department == null)
		//    {
		//        return NotFound();
		//    }
		//    return View(department);
		//}
		[HttpPost]

		public IActionResult Delete(int id)
		{
			var message = string.Empty;

			try
			{
				var employee = _employesService.DeleteEmploye(id);
				if (employee)
				{
					return RedirectToAction(nameof(Index));
				}
				message = "an error has occured during deleting the emploee";
			}
			catch (Exception ex)
			{

				_logger.LogError(ex, ex.Message);

				message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during deleting the emploee";
			}
			//ModelState.AddModelError(string.Empty, message);

			// shoud use torser and use tempedata[]
			return RedirectToAction(nameof(Index));

		}
	}
}
