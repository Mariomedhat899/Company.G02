using AutoMapper;
using Company.G02.BLL.InterFaces;
using Company.G02.BLL.Repositories;
using Company.G02.DAL.Modles;
using Company.G02.PL.DTOS;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.G02.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var department = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {

            if (ModelState.IsValid)
            {
                //var department = new Department()
                //{
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = model.CreateAt
                //};
                var department = _mapper.Map<Department>(model);
               await  _unitOfWork.DepartmentRepository.AddAsync(department);

                var Count = await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {

            if (id == null || id <= 0) return BadRequest("Invalid Id!");

            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

            if (department == null) return NotFound(new { StatusCode = 404, massage = $"The Department With ID:{id} Is Not Found!" });

           


            return View(viewName,department);


        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute]int? id)
        {

            if (id == null || id <= 0) return BadRequest("Invalid Id!");

            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);

            if (department == null) return NotFound(new { StatusCode = 404, massage = $"The Department With ID:{id} Is Not Found!" });

            //var DepartmentDto = new CreateDepartmentDto()
            //{
            //    Id =id.Value,
            //    Code = department.Code,
            //    Name = department.Name,
            //    CreateAt = department.CreateAt
            //};

            var DepartmentDto = _mapper.Map<CreateDepartmentDto>(department);



            return View(DepartmentDto);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, CreateDepartmentDto department)
        {



            if (ModelState.IsValid)
            {
                //if (id != department.Id) return BadRequest("Id Is Not Matched");

                //var _Department = new Department()
                //{
                //    Id = id,
                //    Code = department.Code,
                //    Name = department.Name,
                //    CreateAt = department.CreateAt
                //};
                var _Department = _mapper.Map<Department>(department);
                _unitOfWork.DepartmentRepository.Update(_Department);
                var Count = await _unitOfWork.CompleteAsync();
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }


            return View(department);
        }



        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{

        //    if (id == null || id <= 0) return BadRequest("Invalid Id!");

        //    var department = _department.Get(id.Value);

        //    if (department == null) return NotFound(new { StatusCode = 404, massage = $"The Department With ID:{id} Is Not Found!" });


        //    return View(department);


        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            if(id <= 0) return BadRequest("Invalid Id!");


            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);

            if(department == null) return NotFound(new { StatusCode = 404, massage = $"The Department With ID:{id} Is Not Found!" });

            _unitOfWork.DepartmentRepository.Delete(department);
            var Count = await _unitOfWork.CompleteAsync();

            if (Count > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return StatusCode(500, new { message = "Failed to delete the department. Please try again." });





            //if (ModelState.IsValid)
            //{
            //    if (id != department.Id) return BadRequest("Id Is Not Matched");
            //    var Count = _department.Delete(department);
            //    if (Count > 0)
            //    {
            //        return RedirectToAction(nameof(Index));
            //    }
            //}


            //return View(department);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id <= 0) return BadRequest("Invalid Id!");

        //    var department = _department.Get(id.Value);

        //    if (department == null) return NotFound(new { StatusCode = 404, massage = $"The Department With ID:{id} Is Not Found!" });

        //    var Count = _department.Delete(department);

        //    if (Count > 0)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(id);
        //}

    }
}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        //{



        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department()
        //        {
        //            Id = id,
        //            Code = model.Code,
        //            Name = model.Name,
        //            CreateAt = model.CreateAt
        //        };
        //        if (id != department.Id) return BadRequest("Id Is Not Matched");
        //        var Count = _department.Update(department);
        //        if (Count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }


        //    return View(model);


        //}




