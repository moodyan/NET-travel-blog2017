using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelBlog.Models;

namespace TravelBlog.Controllers
{
    public class LocationController : Controller
    {
        private TravelBlogContext db = new TravelBlogContext();
        //View list of locations
        public IActionResult Index()
        {
            return View(db.Locations.ToList());
        }
        //Details view of location
        public IActionResult Details(int id)
        {
            var thisLocation = db.Locations.FirstOrDefault(location => location.LocationId == id);

            try
            {
                var experiences = db.Experiences
                    .Where(experience => experience.LocationId == id).ToList();
                ViewBag.Experiences = experiences;
                return View(thisLocation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View(thisLocation);
            }
        }
        //Create new Location
        public IActionResult Create()
        {
            ViewBag.ExperienceId = new SelectList(db.Experiences, "ExperienceId", "Title");
            ViewBag.PersonId = new SelectList(db.People, "PersonId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Location location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Edit a Location
        public IActionResult Edit(int id)
        {
            var thisLocation = db.Locations.FirstOrDefault(location => location.LocationId == id);
            ViewBag.ExperienceId = new SelectList(db.Experiences, "ExperienceId", "Title");
            ViewBag.PersonId = new SelectList(db.People, "PersonId", "Name");
            return View(thisLocation);
        }

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            db.Entry(location).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Delete a Location
        public IActionResult Delete(int id)
        {
            var thisLocation = db.Locations.FirstOrDefault(location => location.LocationId == id);
            return View(thisLocation);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisLocation = db.Locations.FirstOrDefault(location => location.LocationId == id);
            db.Locations.Remove(thisLocation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
