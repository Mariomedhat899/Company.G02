using AutoMapper;
using Company.G02.BLL;
using Company.G02.BLL.InterFaces;
using Company.G02.DAL.Modles;
using Company.G02.PL.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace Company.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
       
        public EmployeeController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            

          
        }
        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
             IEnumerable<Employee> employees;

            if (SearchInput is null )
            {
             employees = _unitOfWork.EmployeeRepository.GetAll();
            }else
            {
                 employees = _unitOfWork.EmployeeRepository.GetByName(SearchInput);
            }

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
                //var employee = new Employee()
                //{
                //    Name = model.Name,
                //    Email = model.Email,
                //    HireDate = model.HireDate,
                //    CreateAt = model.CreateAt,
                //    Age = model.Age,
                //    Address = model.Address,
                //    Salary = model.Salary,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    Phone = model.Phone,
                //    DepartmentId = model.DepartmentId

                //};
                var employee = _mapper.Map<Employee>(model);
                _unitOfWork.EmployeeRepository.Add(employee);
                var count = _unitOfWork.Complete();
                if (count > 0)
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

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);

            if (employee == null) return NotFound(new { StatusCode = 404, massage = $"The Employee With ID:{id} Is Not Found!" });


            return View(viewName,employee);
            
        }


        [HttpGet]

        public IActionResult Edit([FromRoute] int? id)
        {

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            if (id == null || id <= 0) return BadRequest("Invalid Id!");


            if (employee == null) return NotFound(new { StatusCode = 404, massage = $"The Employee With ID:{id} Is Not Found!" });
            var dto = _mapper.Map<CreateEmployeeDto>(employee);

            return View(dto);
        }

        [HttpPost]

        public IActionResult Edit([FromRoute] int id, CreateEmployeeDto employee)
        {
            

            if (id == null || id <= 0) return BadRequest("Invalid Id!");
            if (ModelState.IsValid)
            {
                
                //if (id != employee.Id) return BadRequest();
                //var _employee = new Employee()
                //{
                //    Id = id,
                //    Name = employee.Name,
                //    Email = employee.Email,
                //    HireDate = employee.HireDate,
                //    CreateAt = employee.CreateAt,
                //    Age = employee.Age,
                //    Address = employee.Address,
                //    Salary = employee.Salary,
                //    IsActive = employee.IsActive,
                //    IsDeleted = employee.IsDeleted,
                //    Phone = employee.Phone,
                //    DepartmentId = employee.DepartmentId

                //};

               var _employee = _mapper.Map<Employee>(employee);

                _employee.Id = id;



                 _unitOfWork.EmployeeRepository.Update(_employee);

                var count = _unitOfWork.Complete();

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
            var employee = _unitOfWork.EmployeeRepository.Get(id.Value); if (employee == null) return NotFound(new { StatusCode = 404, massage = $"The Employee With ID:{id} Is Not Found!" });

            _unitOfWork.EmployeeRepository.Delete(employee);
            var count = _unitOfWork.Complete();

            if (count > 0)
            {
                return RedirectToAction("Index");
            }

            return StatusCode(500, new { message = "Failed to delete the department. Please try again." });
        }

    }
}
