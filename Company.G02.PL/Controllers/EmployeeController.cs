using AutoMapper;
using Company.G02.BLL;
using Company.G02.BLL.InterFaces;
using Company.G02.DAL.Modles;
using Company.G02.PL.DTOS;
using Company.G02.PL.Helpers;
using GemBox.Document;
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
                    if (model.IMGFile != null) model.IMGName = DoucumentSettings.UploadFile(model.IMGFile, "Images");


            if (ModelState.IsValid)
            {
               
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

                if(employee.IMGName is not null && employee.IMGFile is not null)
                {
                    DoucumentSettings.DeleteFile(employee.IMGName, "Images");
                }

                if(employee.IMGFile != null)
                {
                    employee.IMGName = DoucumentSettings.UploadFile(employee.IMGFile, "Images");
                }       
              

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
                if(employee.IMGName is not null)
                    DoucumentSettings.DeleteFile(employee.IMGName, "Images");

                return RedirectToAction("Index");
            }

            return StatusCode(500, new { message = "Failed to delete the department. Please try again." });
        }

    }
}
