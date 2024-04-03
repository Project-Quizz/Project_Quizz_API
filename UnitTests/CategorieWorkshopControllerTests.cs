using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Project_Quizz_API.Controllers;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models;
using Project_Quizz_API.Models.DTOs;

namespace Project_Quizz_API.UnitTests
{
    [TestFixture]
    public class CategorieWorkshopControllerTests
    {
        private CategorieWorkshopController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetupInMemory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);

            _controller = new CategorieWorkshopController(_context);

            string categorieName = "Vorhanden";
            _controller.CreateQuizCategorie(categorieName);
            categorieName = "Vorhanden2";
            _controller.CreateQuizCategorie(categorieName);
            categorieName = "Vorhanden3";
            _controller.CreateQuizCategorie(categorieName);
        }

        [Test]
        public void CreateQuizCategorie_AddCategorieToDb()
        {
            string categorieName = "Erfolgreich";

            var result = _controller.CreateQuizCategorie(categorieName) as CreatedAtActionResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
            var categorie = _context.Quiz_Categories.FirstOrDefault(x => x.Name == "Erfolgreich");
            ClassicAssert.IsNotNull(categorie);
        }

        [Test]
        public void CreateQuizCategorie_WithExistingName()
        {
            string categorieName = "Vorhanden";

            var result = _controller.CreateQuizCategorie(categorieName) as BadRequestObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            Assert.That(result.Value, Is.EqualTo("A categorie with the same name already exists"));
        }

        [Test]
        public void GetQuizCategorie_WithExistingId()
        {
            int categorieId = 1;

            var result = _controller.GetCategorie(categorieId) as OkObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public void GetQuizCategorie_WithNonExistingId()
        {
            int categorieId = 99;

            var result = _controller.GetCategorie(categorieId) as NotFoundObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        [Test]
        public void GetQuizCategories_WithOkResponse()
        {
            var result = _controller.GetAllCategories() as OkObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }
    }
}
