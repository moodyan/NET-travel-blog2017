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
    public class HomeController : Controller
    {
       private TravelBlogContext db = new TravelBlogContext();
       public IActionResult Index()
       {
           return View();
       }
    }
}
