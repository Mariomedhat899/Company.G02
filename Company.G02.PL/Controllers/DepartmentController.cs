using Company.G02.BLL.InterFaces;
using Company.G02.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.G02.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _department;

        public DepartmentController(IDepartmentRepository department)
        {
            _department = department;
        }

        [HttpGet]
        public IActionResult Index()
        {
           var department =  _department.GetAll();
            return View(department);
        }
    }
}
