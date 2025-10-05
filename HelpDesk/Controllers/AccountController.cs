using HelpDesk.Models;
using HelpDesk.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Controllers
{
    public class AccountController : Controller {
        
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private SignInManager<AppUser> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    if(!await _userManager.IsEmailConfirmedAsync(user)){
                        ModelState.AddModelError("","Please confirm your account");
                        return View(model);
                    }

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user,null);

                        return RedirectToAction("Index","Home");
                    }
                    else if(result.IsLockedOut) {
                        var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                        var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                        ModelState.AddModelError("",$"Your account has been locked, please try again in {timeLeft.Minutes} minutes.");
                    }
                    else {
                        ModelState.AddModelError("","your password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "No account was created with this email address.");
                };
            }
            return View(model);
        }

        public IActionResult Register()
        {   
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail","Account", new {user.Id, token});

                    await _emailSender.SendEmailAsync(user.Email, "Verify Email", $"Please click on the link to confirm your email <a href='http://localhost:5185{url}'> account.</a>");

                    TempData["message"] = "Click on the confirmation email in your email account.";
                    return RedirectToAction("Login","Account");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string Id, string token) {
            if(Id == null || token == null) {
                TempData["message"] = "Unvalid Token";
                return View();
            }

            var user = await _userManager.FindByIdAsync(Id);

            if(user != null) {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if(result.Succeeded){
                    TempData["message"] = "Your account has been approved";
                    return RedirectToAction("Login","Account");
                }
            }
            TempData["message"] = "User not found";
            return View();     
        }

        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}