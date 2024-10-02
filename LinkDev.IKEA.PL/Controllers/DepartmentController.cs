using AutoMapper;
using Link.Dev.IKEA.BLL.Models.Departments;
using Link.Dev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Entites.Departments;
using LinkDev.IKEA.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
namespace LinkDev.IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepratmentService _depratmentService;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IMapper _mapper;

		public DepartmentController(IDepratmentService depratmentService, ILogger<DepartmentController> logger, IWebHostEnvironment webHostEnvironment,IMapper mapper)
        {
            _depratmentService = depratmentService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
			_mapper = mapper;
		}
        [HttpGet]


        public async Task <IActionResult> Index()

        {
            //ViewData["Message"] = "hello view date";
            //ViewBag["Message"] = "heloo view bag";

            var departments = await _depratmentService.GetAllDepartmentsAsynce();
            return View(departments);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]


        public async Task <IActionResult> Create(DepartmentViewModel department)

        {
            if (!ModelState.IsValid)
                return View(department);
            var message = string.Empty;
            try
            {
                var createdDepartment =_mapper.Map<DepartmentViewModel,CreatedDepartmentDto>(department) ;

                var created = await _depratmentService.CreateDepartmentAsynce(createdDepartment) >0 ;

                if (created)
                {
                    TempData["Message"] = "Department is created";
                    return RedirectToAction(nameof(Index));

                }
                else
                {
					message = "Department is not created";
					ModelState.AddModelError(string.Empty, message);
					return View(department);
                   


				}








			}
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during Creating the department";

            }

            return RedirectToAction(nameof(Index));

        }


        public async Task <IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return BadRequest();
			}
			var department = await _depratmentService.GetDepartmentsByIdAsynce(id.Value);
			if (department == null)

			{
				return NotFound();
			}
			return View(department);
		}

        [HttpGet]

        public async Task <IActionResult> Edit(int? id)

        {

            if (id is null)
            {
                return BadRequest();
            }

            var department = await _depratmentService.GetDepartmentsByIdAsynce(id.Value);

            // Check if department exists
            if (department == null)
            {
                return NotFound();  // Return 404 if the department is not found
            }
            var departmentvm = _mapper.Map<DepartmentDetailsToReturnDto, DepartmentViewModel>(department);
            // Pass the fetched department data to the view
            return View(departmentvm);
        }
        [ValidateAntiForgeryToken]

        [HttpPost]

        public async Task <IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVm)

        {
            if (!ModelState.IsValid)
            {
                return View(departmentVm);
            }
            var message = string.Empty;
            try
            {
                var departmenttoUpdate = _mapper.Map<DepartmentViewModel,UpdatedDepartmentDto>(departmentVm) ;
                var updated = await _depratmentService.UpdateDepartmentAsynce(departmenttoUpdate) > 0;
                if (updated)
                {
                    TempData["Message"] = "Department is Updated";
                    return RedirectToAction(nameof(Index));
                }
                else
				{
					TempData["Message"] = "Department is not Updated";


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

        [HttpGet]

        public async Task <IActionResult> Delete(int? id)

        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = await _depratmentService.GetDepartmentsByIdAsynce(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [ValidateAntiForgeryToken]

        [HttpPost]


        public async Task <IActionResult> Delete(int id)

        {
            var message = string.Empty;

		
            

			try
			{

                var deleted =await _depratmentService.DeleteDepartmentAsynce(id);

                if (deleted)
                {
                    TempData["Message"]="The Department is Deleted";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "an error has occured during deleting the department";

                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during deleting the department";
            }
            ModelState.AddModelError(string.Empty, message);

            // shoud use torser and use tempedata[]
            return RedirectToAction(nameof(Index));

        }
        
    }
}