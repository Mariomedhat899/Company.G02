using Company.G02.DAL.Modles;
using Company.G02.PL.DTOS;
using Company.G02.PL.Helpers;
using Company.G02.PL.Helpers.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.G02.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _SigninManager;
        private readonly  IMailService _mailService;



        public AccountController(IMailService mailService, UserManager<AppUser> userManager, SignInManager<AppUser> SigninManager)
        {
            _userManager = userManager;
            _SigninManager = SigninManager;
            _mailService = mailService;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user is null)
                {

                    user = await _userManager.FindByEmailAsync(model.Email);

                    if (user is null)
                    {
                        user = new AppUser()
                        {
                            Email = model.Email,
                            UserName = model.Username,
                            FName = model.FName,
                            LName = model.LName,
                            IsAgree = model.IsAgree

                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("SignIn");
                        }
                        foreach (var err in result.Errors)
                        {
                            ModelState.AddModelError("", err.Description);
                        }

                    }
                }
                ModelState.AddModelError("", "Invalid Sign Up !!");



            }

            return View(model);


        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);

                    if (flag)
                    {
                        var Result = await _SigninManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                        if (Result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                ModelState.AddModelError("", "Invalid Sign In !!");

            }
            return View(model);
        }

        public new async Task<IActionResult> SignOut()
        {
            await _SigninManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"]?.ToString();
                var token = TempData["token"]?.ToString();

                if (email is null || token is null)
                {
                    return BadRequest("Invalid Operation !");
                }
                var user = await _userManager.FindByEmailAsync(email);

                if(user is not null)
                {
                 var Result = await   _userManager.ResetPasswordAsync(user, token, model.NewPassword);

                    if (Result.Succeeded) {

                        return RedirectToAction(nameof(SignIn));
                    }

                }
                ModelState.AddModelError("", "Invaild Operation Please Try Again!");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token }, Request.Scheme);

                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = url
                    };

                    //var flag = EmailSettings.SendEmail(email);
                    _mailService.SendEmail(email);
                        return RedirectToAction("CheckYourInbox");
                    

                }


            }
            ModelState.AddModelError("", "Invalid Email !!");
            return View(nameof(ForgetPassword), model);
        }

        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }
    }
}
