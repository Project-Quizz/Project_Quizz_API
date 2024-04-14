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
    /// <summary>
    /// Unti tests for the CategorieWorkshopController.
    /// </summary>
    [TestFixture]
    public class CategorieWorkshopControllerTests
    {
        private CategorieWorkshopController _controller;
        private ApplicationDbContext _context;

        /// <summary>
        /// Setup the in memory database.
        /// </summary>
        [SetUp]
        public void SetupInMemory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);

            _context.Database.EnsureDeleted();

            _context.Database.EnsureCreated();

            _controller = new CategorieWorkshopController(_context);

            string categorieName = "Vorhanden";
            _controller.CreateQuizCategorie(categorieName);
            categorieName = "Vorhanden2";
            _controller.CreateQuizCategorie(categorieName);
            categorieName = "Vorhanden3";
            _controller.CreateQuizCategorie(categorieName);
        }

        /// <summary>
        /// Test the CreateQuizCategorie method.
        /// </summary>
        [Test]
        public void CreateQuizCategorie_AddCategorieToDb()
        {
            string categorieName = "Erfolgreich";

            var result = _controller.CreateQuizCategorie(categorieName) as CreatedAtActionResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
            var categorie = _context.Quiz_Categories.FirstOrDefault(x => x.Name == "Erfolgreich");
            ClassicAssert.IsNotNull(categorie);
        }

        /// <summary>
        /// Test the CreateQuizCategorie method with an existing name.
        /// </summary>
        [Test]
        public void CreateQuizCategorie_WithExistingName()
        {
            string categorieName = "Vorhanden";

            var result = _controller.CreateQuizCategorie(categorieName) as BadRequestObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            Assert.That(result.Value, Is.EqualTo("A categorie with the same name already exists"));
        }

        /// <summary>
        /// Test the GetCategorie method with an existing id.
        /// </summary>
        [Test]
        public void GetQuizCategorie_WithExistingId()
        {
            int categorieId = 1;

            var result = _controller.GetCategorie(categorieId) as OkObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        /// <summary>
        /// Test the GetCategorie method with a non-existing id.
        /// </summary>
        [Test]
        public void GetQuizCategorie_WithNonExistingId()
        {
            int categorieId = 99;

            var result = _controller.GetCategorie(categorieId) as NotFoundObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        /// <summary>
        /// Test the GetAllCategories method.
        /// </summary>
        [Test]
        public void GetQuizCategories_WithOkResponse()
        {
            var result = _controller.GetAllCategories() as OkObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        /// <summary>
        /// Test the DeleteQuizCategorie method with an existing id.
        /// </summary>
        [Test]
        public void CreateQuizCategorie_EmptyName()
        {
            string emptyCategorieName = "";

            var result = _controller.CreateQuizCategorie(emptyCategorieName) as BadRequestObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            Assert.That(result.Value, Is.EqualTo("Categorie name cant be null or empty"));
        }

        /// <summary>
        /// Test the DeleteQuizCategorie method with an existing id.
        /// </summary>
        [Test]
        public void DeleteQuizCategorie_NonExistentCategory()
        {
            int nonExistentCategorieId = 9999;

            var result = _controller.DeleteQuizCategorie(nonExistentCategorieId) as NotFoundObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
            Assert.That(result.Value, Is.EqualTo($"Categorie with Id {nonExistentCategorieId} not Found"));
        }

        /// <summary>
        /// Test the DeleteQuizCategorie method with an existing id.
        /// </summary>
        [Test]
        public void DeleteQuizCategorie_CriticalCategory()
        {
            int criticalCategorieId = 1;

            var result = _controller.DeleteQuizCategorie(criticalCategorieId) as BadRequestObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            Assert.That(result.Value, Is.EqualTo($"Id {criticalCategorieId} cannot be deleted due to development settings"));
        }
    }
}
