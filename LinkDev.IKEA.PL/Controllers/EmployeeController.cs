using Link.Dev.IKEA.BLL.Models.Departments;
using Link.Dev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Servcies.Employees;
using LinkDev.IKEA.DAL.Entites.Employees;
using LinkDev.IKEA.PL.ViewModels;
using LinkDev.IKEA.PL.ViewModels.Employee;
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

		public IActionResult Index(string search)
		{
			var employees = _employesService.GetEmployes( search);


			return View(employees);
		}

		[HttpGet]
		public IActionResult Search (string search)
		{
			var employees = _employesService.GetEmployes(search);


			return PartialView("Partial/EmployeesSearchByAjax", employees);
		}

		[HttpGet]

		// injection Idepartmentservice at view or PartialView
		public IActionResult Create(/*[FromServices] IDepratmentService depratmentService*/)
		{
			//ViewData["Departments"] = depratmentService.GetAllDepartments();
			return View();
		}
		
		//[ValidateAntiForgeryToken]
		[HttpPost]
		public IActionResult Create(EmploeeViewModel emploeeView)
		{
			var Employee = new CreatedEmployeeDto()
			{
				Name= emploeeView.Name,
				Gender= emploeeView.Gender,
				DepartmentId= emploeeView.DepartmentId,
				Adress	= emploeeView.Adress,
				Age	= emploeeView.Age,
				Email	= emploeeView.Email,
				EmployeeType	= emploeeView.EmployeeType,
				HiringDate	= emploeeView.HiringDate,
				IsActive = emploeeView.IsActive,
					PhoneNumber = emploeeView.PhoneNumber,
					Salary= emploeeView.Salary,
					Image= emploeeView.Image,
			};


			if (!ModelState.IsValid)
				return View(emploeeView);
			var message = string.Empty;
			try
			{
				var result = _employesService.CreateEmploye(Employee);
				if (result > 0)
				{
					TempData["Message"] = "Employee Is Created";

					return RedirectToAction(nameof(Index));

				}
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
					return View(emploeeView);
				}
				else
				{
					message = "Employee is not Created";
					return View("Error", message);
				}
			}
		}
		[HttpGet]
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

        [HttpGet]
		public IActionResult Edit(int? id,[FromServices] IDepratmentService depratmentService)
		{

			if (id is null)
			{
				return BadRequest();
			}

			var employee = _employesService.GetEmployesById(id.Value);

			// Check if department exists
			if (employee == null)
			{
				return NotFound();  // Return 404 if the department is not found
			}

            ViewData["Departments"] = depratmentService.GetAllDepartments();


            // Pass the fetched department data to the view
            return View(new EmploeeViewModel
            {
				//Id = employee.Id,
				Name = employee.Name,
			
				Adress=employee.Adress,
				Email=employee.Email,
				Age=employee.Age,
				Salary=employee.Salary,
				HiringDate=employee.HiringDate,
				IsActive=employee.IsActive,
				PhoneNumber=employee.PhoneNumber,
				EmployeeType=employee.EmployeeType,
				Gender	=employee.Gender,



			});
		}
        [ValidateAntiForgeryToken]

        [HttpPost]
		public IActionResult Edit([FromRoute] int id, EmploeeViewModel emploeeView)
		{

			var employee = new UpdatedEmployeeDto()
			{
Adress=emploeeView.Adress,
Age=emploeeView.Age,
	DepartmentId=emploeeView.DepartmentId,
	Email=emploeeView.Email,
	EmployeeType	= emploeeView.EmployeeType,
	Gender =emploeeView.Gender,
	HiringDate =emploeeView.HiringDate,
	IsActive=emploeeView.IsActive,
		PhoneNumber=emploeeView.PhoneNumber,
				Name=emploeeView.Name,
					Id	=id,
				Salary=emploeeView.Salary,
			};

			if (!ModelState.IsValid)
			{
				return View(emploeeView);
			}
			var message = string.Empty;
			try
			{
				var updated = _employesService.UpdateEmploye(employee) > 0;

				if (updated)
				{
					TempData["Message"] = "Employee Is Updated";
					return RedirectToAction(nameof(Index));
				}
				else
				{
					TempData["Message"] = "Employee Is Not Updated";



				}

			}
			catch (Exception ex)
			{

				_logger.LogError(ex, ex.Message);

				message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during updating the employee";

			}
			ModelState.AddModelError(string.Empty, message);
			return View(emploeeView);
		}
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //	if (id == null)
        //	{
        //		return BadRequest();
        //	}
        //	var department = _employesService.GetEmployesById(id.Value);
        //	if (department == null)
        //	{
        //		return NotFound();
        //	}
        //	return View(department);
        //}


        //[ValidateAntiForgeryToken]

        [HttpPost]

		public IActionResult Delete(int id)
		{
			var message = string.Empty;

			try
			{
				var employee = _employesService.DeleteEmploye(id);
				if (employee)
				{
					TempData["Message"] = "Employee Is Deleted";
					return RedirectToAction(nameof(Index));
				}
				else
				{
					TempData["Message"] = "Employee Is Not Deleted";

					message = "an error has occured during deleting the emploee";

				}
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
