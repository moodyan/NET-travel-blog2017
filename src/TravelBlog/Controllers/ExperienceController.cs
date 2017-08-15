using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TravelBlog.Controllers
{
    public class ExperienceController : Controller
    {
        private TravelBlogContext db = new TravelBlogContext();
        //View List Of Experiences
        public IActionResult Index()
        {
            return View(db.Experiences.Include(something => something.Location).ToList());
        }
        public IActionResult Details(int id)
        {
            var thisExperience = db.Experiences.Include(experience => experience.Comments).FirstOrDefault(something => something.ExperienceId == id);

            var foundLocation = db.Locations.FirstOrDefault(something => something.LocationId == thisExperience.LocationId);
            ViewBag.Location = foundLocation.Name;
            ViewBag.LocationId = thisExperience.LocationId;
           
            return View(thisExperience);         
        }
        public IActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Experience experience)
        {
            db.Experiences.Add(experience);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
