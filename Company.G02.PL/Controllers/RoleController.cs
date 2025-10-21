using Company.G02.DAL.Modles;
using Company.G02.PL.DTOS;
using Company.G02.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Threading.Tasks;

namespace Company.G02.PL.Controllers
{
    [Authorize]

    public class RoleController(RoleManager<IdentityRole> _rolesManager,UserManager<AppUser> _userManager) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> Roles;

            if (SearchInput is null)
            {
                Roles = _rolesManager.Roles.Select(r => new RoleToReturnDto()
                {
                    Id = r.Id,
                   Name = r.Name

                });
            }
            else
            {
                Roles = _rolesManager.Roles.Select(r => new RoleToReturnDto()
                {
                    Id = r.Id,
                    Name = r.Name

                }).Where(r => r.Name.ToLower().Contains(SearchInput.ToLower()));
            }

            return View(Roles);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            


            if (ModelState.IsValid)
            {

                var Role = await _rolesManager.FindByNameAsync(model.Name);

                if(Role is null)
                {
                    Role = new IdentityRole()
                    {
                        Name = model.Name
                    };

                 var result =await _rolesManager.CreateAsync(Role);
                    if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {

            if (id == null) return BadRequest("Invalid Id!");

            var Role = await _rolesManager.FindByIdAsync(id);

            if (Role == null) return NotFound(new { StatusCode = 404, massage = $"The Role With ID:{id} Is Not Found!" });

            var dto = new RoleToReturnDto()
            {
                Id = Role.Id,
                Name = Role.Name
            };


            return View(viewName, dto);

        }


        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit([FromRoute] string? id)
        {
            if (id == null) return BadRequest("Invalid Id!");
            var Role = await _rolesManager.FindByIdAsync(id);

            if (id == null) return BadRequest("Invalid Id!");


            if (Role == null) return NotFound(new { StatusCode = 404, massage = $"The Role With ID:{id} Is Not Found!" });
            var dto = new RoleToReturnDto()
            {
                Id = Role.Id,
                Name = Role.Name
            };

            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto _Role)
        {


            if (id == null) return BadRequest("Invalid Id!");
            if (ModelState.IsValid)
            {

                var Role = await _rolesManager.FindByIdAsync(id);

                if (Role == null) return NotFound("Invalid Operation!!");

                Role.Id = _Role.Id;
                Role.Name = _Role.Name;





                var result = await _rolesManager.UpdateAsync(Role);



                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(_Role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete([FromRoute] string? id)
        {
            if (id is null) return BadRequest("Invalid Id!");
            var Role = await _rolesManager.FindByIdAsync(id); if (Role == null) return NotFound(new { StatusCode = 404, massage = $"The Role With ID:{id} Is Not Found!" });

            var result = await _rolesManager.DeleteAsync(Role);


            if (result.Succeeded)
            {


                return RedirectToAction("Index");
            }

            return StatusCode(500, new { message = "Failed to delete the department. Please try again." });
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {
            var role = await _rolesManager.FindByIdAsync(RoleId);

            if (role is null) return NotFound(new { StatusCode = 404, massage = $"The Role With ID:{RoleId} Is Not Found!" });

            ViewData["RoleId"] = RoleId;


            var usersInRoleVMs = new List<UsersInRoleViewModel>();

           var users = await _userManager.Users.ToListAsync();

            foreach(var user in users)
            {
                var userInRole = new UsersInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }
            usersInRoleVMs.Add(userInRole);
            }
            return View(usersInRoleVMs);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId,List<UsersInRoleViewModel> users) {
            var role = await _rolesManager.FindByIdAsync(RoleId);

            if (role is null) return NotFound(new { StatusCode = 404, massage = $"The Role With ID:{RoleId} Is Not Found!" });


            if (ModelState.IsValid)
            {
               foreach(var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                  if(appUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser,role.Name))
                        {
                           await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                           await _userManager.RemoveFromRoleAsync(appUser,role.Name);

                        }
                    }
                }
                  return RedirectToAction(nameof(Edit),new {Id = RoleId});
            }

            return View(users);
        }
    }
}
