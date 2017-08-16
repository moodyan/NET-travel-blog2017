using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TravelBlog.Models;
using TravelBlog.ViewModels;

namespace TravelBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly TravelBlogContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TravelBlogContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        //public AccountController() { }

        public UserManager<ApplicationUser> GetUser()
        {
            return _userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
                return await LoginOnRegister(user, model.Password);
            }
            else
            {
                return View("Index", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(RegisterViewModel model, string adminPassword)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded && adminPassword == "Admin123!")
            {
                await _userManager.AddToRoleAsync(user, "admin");
                return await LoginOnRegister(user, model.Password);
            }
            else
            {
                return View("Index", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoginOnRegister(ApplicationUser user, string password)
        {

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.Email, password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Register");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult HelloAjax()
        {
            return Content("Hello from the controller!", "text/plain");
        }

    }
}
