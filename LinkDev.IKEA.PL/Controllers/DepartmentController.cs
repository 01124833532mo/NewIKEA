using Link.Dev.IKEA.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;
namespace LinkDev.IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepratmentService _depratmentService;
        public DepartmentController(IDepratmentService depratmentService)
        {
            _depratmentService = depratmentService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}