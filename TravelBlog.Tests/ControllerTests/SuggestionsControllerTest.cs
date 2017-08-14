using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelBlog.Models;
using TravelBlog.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TravelBlog.Models.Repositories;
using TravelBlog.Tests.ModelTests;
using Microsoft.EntityFrameworkCore;

namespace TravelBlog.Tests.ControllerTests
{
    public class SuggestionsControllerTest : IDisposable
    {
        Mock<ISuggestionRepository> mock = new Mock<ISuggestionRepository>();
        EFSuggestionRepository db = new EFSuggestionRepository(new TestDbContext());
        private void DbSetup()
        {
            mock.Setup(m => m.Suggestions).Returns(new Suggestion[]
            {
                new Suggestion { SuggestionId = 1, Author = "Alyssa", Description = "Its.. France", Place = "Fukken France", Visited = false, Votes = 0},
                new Suggestion { SuggestionId = 2, Author = "David", Description = "Lentils and fukken couscous", Place = "Tunisia", Visited = true, Votes = 0},
                new Suggestion { SuggestionId = 3, Author = "Guy", Description = "Camping on the coast!", Place = "The Coast", Visited = false, Votes = 0}
            }.AsQueryable());
        }
        //[Fact]
        //public void Post_MethodAddsSuggestion_Test() //redundant, uses wrong database - see Mock_ConfirmEntry_Test()
        //{
        //    //Arrange
        //    SuggestionsController controller = new SuggestionsController();
        //    Suggestion testSuggestion = new Suggestion();
        //    testSuggestion.Place = "Fukken France";

        //    //Act
        //    controller.Create(testSuggestion);
        //    ViewResult indexView = new SuggestionsController().Index() as ViewResult;
        //    var collection = indexView.ViewData.Model as IEnumerable<Suggestion>;

        //    //Assert
        //    Assert.Contains<Suggestion>(testSuggestion, collection);
        //}
        [Fact]
        public void Get_ModelListSuggestionIndex_Test()
        {
            //Arrange
            ViewResult indexView = new SuggestionsController(mock.Object).Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsType<List<Suggestion>>(result);
        }
        [Fact]
        public void Mock_GetViewResultIndex_Test() //Confirms route returns view
        {
            //Arrange
            DbSetup();
            SuggestionsController controller = new SuggestionsController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Mock_IndexListOfSuggestion_Test() //Confirms model as list of items
        {
            // Arrange
            DbSetup();
            ViewResult indexView = new SuggestionsController(mock.Object).Index() as ViewResult;

            // Act
            var result = indexView.ViewData.Model;

            // Assert
            Assert.IsType<List<Suggestion>>(result);
        }

        [Fact]
        public void Mock_ConfirmEntry_Test() //Confirms presence of known entry
        {
            // Arrange
            DbSetup();
            SuggestionsController controller = new SuggestionsController(mock.Object);
            Suggestion testSuggestion = new Suggestion();
            testSuggestion.Description = "Its.. France";
            testSuggestion.SuggestionId = 1;

            // Act
            ViewResult indexView = controller.Index() as ViewResult;
            var collection = indexView.ViewData.Model as IEnumerable<Suggestion>;

            // Assert
            Assert.Contains<Suggestion>(testSuggestion, collection);
        }
        [Fact]
        public void DB_CreateNewEntry_Test()
        {
            // Arrange
            SuggestionsController controller = new SuggestionsController(db);
            Suggestion testSuggestion = new Suggestion();
            testSuggestion.Description = "TestDb Suggestion";

            // Act
            controller.Create(testSuggestion);
            var collection = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Suggestion>;

            // Assert
            Assert.Contains<Suggestion>(testSuggestion, collection);
        }
        [Fact]
        public void Mock_ConfirmUpvote_Test()
        {
            //AAAARRRRANNNNNGGEEE
            SuggestionsController controller = new SuggestionsController(db);
            Suggestion testSuggestion = new Suggestion();
            testSuggestion.Votes = 1;

            // Aaaaaaaaaaaaaaaaaaaaaaaaact
            db.Save(testSuggestion);
            db.Upvote(testSuggestion);

            // Aaaaaaaaaaaaaasserrrrt
            Assert.Equal(2, testSuggestion.Votes);
        }

        public void Dispose()
        {
            db.DeleteAll();
        }
    }
}
