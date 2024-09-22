using Link.Dev.IKEA.BLL.Models.Departments;
using Link.Dev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Entites.Departments;
using LinkDev.IKEA.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
namespace LinkDev.IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepratmentService _depratmentService;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DepartmentController(IDepratmentService depratmentService, ILogger<DepartmentController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _depratmentService = depratmentService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]

        public IActionResult Index()
        {
            //ViewData["Message"] = "hello view date";
            //ViewBag["Message"] = "heloo view bag";

            var departments = _depratmentService.GetAllDepartments();
            return View(departments);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Create(DepartmentViewModel department)
        {
            if (!ModelState.IsValid)
                return View(department);
            var message = string.Empty;
            try
            {
                var createdDepartment = new CreatedDepartmentDto
                {
                    Code = department.Code,
                    Name = department.Name,
                    CreationDate = department.CreationDate,
                    Description = department.Description

                };
                var created = _depratmentService.CreateDepartment(createdDepartment) >0 ;
                if (!created)
                {
                    TempData["Message"] = "Department Is not Created";
                }


                TempData["Message2"] = "Department is created";
					ModelState.AddModelError(string.Empty, message);
					return View(department);
				
			





            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Department is not created";
                TempData["Message"] = message;
                return RedirectToAction(nameof(Index));
            }
        }

		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			var department = _depratmentService.GetDepartmentsById(id.Value);
			if (department == null)
			{
				return NotFound();
			}
			return View(department);
		}

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id is null)
            {
                return BadRequest();
            }

            var department = _depratmentService.GetDepartmentsById(id.Value);

            // Check if department exists
            if (department == null)
            {
                return NotFound();  // Return 404 if the department is not found
            }

            // Pass the fetched department data to the view
            return View(new DepartmentViewModel
            {
                Code = department.Code,
                //Id = department.Id,
                Name = department.Name,
                CreationDate = department.CreationDate,
                Description = department.Description,




            });
        }
        [ValidateAntiForgeryToken]

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVm)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVm);
            }
            var message = string.Empty;
            try
            {
                var departmenttoUpdate = new UpdatedDepartmentDto
                {
                    Code = departmentVm.Code,
                    Id = id,
                    Name = departmentVm.Name,
                    CreationDate = departmentVm.CreationDate,
                    Description = departmentVm.Description

                };
                var updated = _depratmentService.UpdateDepartment(departmenttoUpdate) > 0;
                if (updated)
                {
                    return RedirectToAction(nameof(Index));
                }

                message = "an error has occured during updating the department";
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during updating the department";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVm);
        }
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

        [ValidateAntiForgeryToken]

        [HttpPost]

        public IActionResult Delete(int id)
        {
            var message = string.Empty;

		
            

			try
			{
                var deleted = _depratmentService.DeleteDepartment(id);
                if (deleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "an error has occured during deleting the department";
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during deleting the department";
            }
            //ModelState.AddModelError(string.Empty, message);

            // shoud use torser and use tempedata[]
            return RedirectToAction(nameof(Index));

        }
        
    }
}