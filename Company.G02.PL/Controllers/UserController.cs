using Company.G02.BLL;
using Company.G02.DAL.Modles;
using Company.G02.PL.DTOS;
using Company.G02.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.G02.PL.Controllers
{
    [Authorize]

    public class UserController(UserManager<AppUser> _userManager) :Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReturnDto> users;

            if (SearchInput is null)
            {
               users =  _userManager.Users.Select(u => new UserToReturnDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FName = u.FName,
                    LName = u.LName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result

                });
            }
            else
            {
                users = _userManager.Users.Select(u => new UserToReturnDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FName = u.FName,
                    LName = u.LName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result

                }).Where(u => u.UserName.ToLower().Contains(SearchInput.ToLower()));
            }

            return View(users);
        }



        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {

            if (id == null ) return BadRequest("Invalid Id!");

            var user = await _userManager.FindByIdAsync(id);

            if (user == null) return NotFound(new { StatusCode = 404, massage = $"The User With ID:{id} Is Not Found!" });

            var dto = new UserToReturnDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles =  _userManager.GetRolesAsync(user).Result
            };


            return View(viewName, dto);

        }


        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit([FromRoute] string? id)
        {
            if (id == null ) return BadRequest("Invalid Id!");
            var user = await _userManager.FindByIdAsync(id);

            if (id == null ) return BadRequest("Invalid Id!");


            if (user == null) return NotFound(new { StatusCode = 404, massage = $"The User With ID:{id} Is Not Found!" });
            var dto = new UserToReturnDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };

            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturnDto _user)
        {


            if (id == null ) return BadRequest("Invalid Id!");
            if (ModelState.IsValid)
            {

               var user= await _userManager.FindByIdAsync(id);

                if (user == null) return NotFound( "Invalid Operation!!");

                user.UserName = _user.UserName;
                user.FName = _user.FName;
                user.LName = _user.LName;
                user.Email = _user.Email;
            




                var result = await  _userManager.UpdateAsync(user);

               

                if (result.Succeeded )
                {
                    return RedirectToAction("Index");
                }
            }
            return View(_user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete([FromRoute] string? id)
        {
            if (id is null ) return BadRequest("Invalid Id!");
            var user = await _userManager.FindByIdAsync(id); if (user == null) return NotFound(new { StatusCode = 404, massage = $"The User With ID:{id} Is Not Found!" });

           var result = await _userManager.DeleteAsync(user);
            

            if (result.Succeeded)
            {
               

                return RedirectToAction("Index");
            }

            return StatusCode(500, new { message = "Failed to delete the department. Please try again." });
        }


    }
}
