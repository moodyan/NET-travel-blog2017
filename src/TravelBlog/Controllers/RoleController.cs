using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TravelBlog.Controllers
{
    public class RoleController : Controller
    {
        private readonly TravelBlogContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RoleController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TravelBlogContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        private TravelBlogContext db = new TravelBlogContext();
        public IActionResult Index()
        {
            var roles = db.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string RoleName)
        {
            try
            {
                db.Roles.Add(new IdentityRole()
                {
                    Name = RoleName
                });
                db.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Delete(string RoleName)
        {
            var thisRole = _db.Roles.FirstOrDefault(r => r.Name == RoleName);
            _db.Roles.Remove(thisRole);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string roleId)
        {
            var thisRole = _db.Roles.FirstOrDefault(r => r.Id == roleId);

            return View(thisRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                var thisRole = _db.Roles.FirstOrDefault(r => r.Id == role.Id);
                thisRole.Name = role.Name;
                _db.Entry(thisRole).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return View();
            }
        }

        public ActionResult ManageUserRoles()
        {
            var list = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        //   [HttpPost]
        //   [ValidateAntiForgeryToken]
        //   public Task<IActionResult> RoleAddToUser(string UserName, string RoleName)
        //   {
        //    try
        //    {
        //        ApplicationUser user = _db.Users.FirstOrDefault(u => u.UserName == UserName);
        //        var task = await _userManager.AddToRoleAsync(user, RoleId);

        //    }
        //    catch (Exception ex)
        //    {
        //        return View();
        //    }
        //    ViewBag.ResultMessage = "Role created successfully !";

        //    // prepopulat roles for the view dropdown
        //    var list = _db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Id.ToString(), Text = rr.Name }).ToList();
        //    ViewBag.Roles = list;

        //    return View("ManageUserRoles");
        //}
    }
}
