using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelBlog.Controllers;
using Xunit;
using Moq;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelBlog.Tests.ModelTests
{
    public class TestDbContext : TravelBlogContext
    {
        public override DbSet<Suggestion> Suggestions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TravelBlogTest;integrated security = True");
        }
    }
}
