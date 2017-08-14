using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelBlog.Models;
using TravelBlog.Models.Repositories;


namespace TravelBlog.Controllers
{
    public class SuggestionsController : Controller
    {
        private ISuggestionRepository suggestionRepo;

        public SuggestionsController(ISuggestionRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.suggestionRepo = new EFSuggestionRepository();
            }
            else
            {
                this.suggestionRepo = thisRepo;
            }
        }


        //View list of suggestions
        public IActionResult Index()
        {
            return View(suggestionRepo.Suggestions.ToList());
        }
        //Details view of suggestion
        public IActionResult Details(int id)
        {
            Suggestion thisSuggestion = suggestionRepo.Suggestions.FirstOrDefault(suggestion => suggestion.SuggestionId == id);
            return View(thisSuggestion);

        }
        //Create new Suggestion
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Suggestion suggestion)
        {
            suggestionRepo.Save(suggestion);
            return RedirectToAction("Index");
        }
        //Edit a Suggestion
        public IActionResult Edit(int id)
        {
            Suggestion thisSuggestion = suggestionRepo.Suggestions.FirstOrDefault(suggestion => suggestion.SuggestionId == id);
            return View(thisSuggestion);
        }

        [HttpPost]
        public IActionResult Edit(Suggestion suggestion)
        {
            suggestionRepo.Edit(suggestion);
            return RedirectToAction("Index");
        }
        //Delete a Suggestion
        public IActionResult Delete(int id)
        {
            Suggestion thisSuggestion = suggestionRepo.Suggestions.FirstOrDefault(suggestion => suggestion.SuggestionId == id);
            return View(thisSuggestion);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Suggestion thisSuggestion = suggestionRepo.Suggestions.FirstOrDefault(suggestion => suggestion.SuggestionId == id);
            suggestionRepo.Remove(thisSuggestion);
            return RedirectToAction("Index");
        }
    }
}
