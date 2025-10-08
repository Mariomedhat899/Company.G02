using Company.G02.BLL.InterFaces;
using Company.G02.BLL.Repositories;
using Company.G02.DAL.Modles;
using Company.G02.PL.DTOS;
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
            var department = _department.GetAll();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateDepartmentDto model)
        {

            if (ModelState.IsValid)
            {
                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
                var Count = _department.Add(department);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [HttpGet]

        public IActionResult Details(int? id)
        {

            if (id == null || id <= 0) return BadRequest("Invalid Id!");

            var department = _department.Get(id.Value);

            if (department == null) return NotFound(new { StatusCode = 404, massage = $"The Department With ID:{id} Is Not Found!" });


            return View(department);






        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null || id <= 0) return BadRequest("Invalid Id!");

            var department = _department.Get(id.Value);

            if (department == null) return NotFound(new { StatusCode = 404, massage = $"The Department With ID:{id} Is Not Found!" });


            return View(department);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {



            if (ModelState.IsValid)
            {
                if (id != department.Id) return BadRequest("Id Is Not Matched");
                var Count = _department.Update(department);
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
        public IActionResult Delete([FromRoute] int id)
        {

            if(id <= 0) return BadRequest("Invalid Id!");


            var department = _department.Get(id);

            if(department == null) return NotFound(new { StatusCode = 404, massage = $"The Department With ID:{id} Is Not Found!" });

            var Count = _department.Delete(department);

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




