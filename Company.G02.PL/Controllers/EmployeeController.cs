using Company.G02.BLL.InterFaces;
using Company.G02.DAL.Modles;
using Company.G02.PL.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace Company.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    HireDate = model.HireDate,
                    CreateAt = model.CreateAt,
                    Age = model.Age,
                    Address = model.Address,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone

                };
                var Count = _employeeRepository.Add(employee);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
          
                       return View(model);
        }


        [HttpGet]
        public IActionResult Details(int? id,string viewName ="Details")
        {

            if (id == null || id <= 0) return BadRequest("Invalid Id!");

            var employee = _employeeRepository.Get(id.Value);

            if (employee == null) return NotFound(new { StatusCode = 404, massage = $"The Employee With ID:{id} Is Not Found!" });


            return View(viewName,employee);
            
        }


        [HttpGet]

        public IActionResult Edit([FromRoute] int? id)
        {
            if (id == null || id <= 0) return BadRequest("Invalid Id!");

            var employee = _employeeRepository.Get(id.Value);

            if (employee == null) return NotFound(new { StatusCode = 404, massage = $"The Department With ID:{id} Is Not Found!" });


            return View(employee);
        }

        [HttpPost]

        public IActionResult Edit([FromRoute] int? id, Employee employee)
        {

            if (ModelState.IsValid)
            {
                if (id != employee.Id) return BadRequest();

                var count = _employeeRepository.Update(employee);

                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
                return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int? id)
        {
            if (id <= 0) return BadRequest("Invalid Id!");
            var employee = _employeeRepository.Get(id.Value); if (employee == null) return NotFound(new { StatusCode = 404, massage = $"The Employee With ID:{id} Is Not Found!" });

            var count = _employeeRepository.Delete(employee);

            if (count > 0)
            {
                return RedirectToAction("Index");
            }

            return StatusCode(500, new { message = "Failed to delete the department. Please try again." });
        }

    }
}
