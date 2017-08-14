using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TravelBlog.Models.Repositories
{
    public class EFSuggestionRepository : ISuggestionRepository
    {
        TravelBlogContext db = new TravelBlogContext();

        public IQueryable<Suggestion> Suggestions { get { return db.Suggestions; } }

        public EFSuggestionRepository(TravelBlogContext connection = null)
        {
            if (connection == null)
            {
                this.db = new TravelBlogContext();
            }
            else
            {
                this.db = connection;
            }
        }

        public Suggestion Save(Suggestion suggestion)
        {
            db.Suggestions.Add(suggestion);
            db.SaveChanges();
            return suggestion;
        }

        public Suggestion Edit(Suggestion suggestion)
        {
            db.Entry(suggestion).State = EntityState.Modified;
            db.SaveChanges();
            return suggestion;
        }

        public void Remove(Suggestion suggestion)
        {
            db.Suggestions.Remove(suggestion);
            db.SaveChanges();
        }

        public void DeleteAll()
        {
            db.RemoveRange(db.Suggestions);
            db.SaveChanges();
        }

        public Suggestion Upvote(Suggestion suggestion)
        {
            suggestion.Votes += 1;
            db.Entry(suggestion).State = EntityState.Modified;
            db.SaveChanges();
            return suggestion;
        }
    }
}
