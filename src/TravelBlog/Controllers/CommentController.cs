using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TravelBlog.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace TravelBlog.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly TravelBlogContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(UserManager<ApplicationUser> userManager, TravelBlogContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int id)
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Comment comment, int id)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            comment.User = currentUser;

            comment.Experience = _db.Experiences.FirstOrDefault(Experiences => Experiences.ExperienceId == id);
            _db.Comments.Add(comment);
            _db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Comments ON");
            _db.SaveChanges();
            _db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Comments OFF");

            return RedirectToAction("Details", "Experience", new { id = id });
        }

        //[HttpPost]
        //public IActionResult CreateComment(Comment comment, string userId, int id)
        //{

        //    comment.User.Id =userId;

        //    comment.Experience = _db.Experiences.FirstOrDefault(Experiences => Experiences.ExperienceId == id);
        //    _db.Comments.Add(comment);
        //    _db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Comments ON");
        //    _db.SaveChanges();
        //    _db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Comments OFF");

        //    return RedirectToAction("Details", "Experience", new { id = id });
        //}


    }
}