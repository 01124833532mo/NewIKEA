using Link.Dev.IKEA.BLL.Models.Departments;
using Link.Dev.IKEA.BLL.Services.Departments;
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
            var departments = _depratmentService.GetAllDepartments();
            return View(departments);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
                return View(department);
            var message = string.Empty;
            try
            {
                var result = _depratmentService.CreateDepartment(department);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Department is not Created";
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
                    return View(department);
                }
                else
                {
                    message = "Department is not Created";
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
			var department = _depratmentService.GetDepartmentsById(id.Value);
			if (department == null)
			{
				return NotFound();
			}
			return View(department);
		}

	}
}