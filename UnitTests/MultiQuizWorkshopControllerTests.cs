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
    public class MultiQuizWorkshopControllerTests
    {
        private MultiQuizWorkshopController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetupInMemory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);

            _context.Database.EnsureDeleted();

            _context.Database.EnsureCreated();

            _controller = new MultiQuizWorkshopController(_context);
        }

        [SetUp]
        public void AddDataToInMemory()
        {
            _context.Quiz_Categories.Add(new Quiz_Categorie {Name = "Test Category" });
            _context.Quiz_Questions.Add(new Quiz_Question { QuestionText = "Test Question", Answers = new List<Quiz_Question_Answer>(), QuizCategorieId = 1, UserId = "User5" });
            _context.Quiz_Questions.Add(new Quiz_Question { QuestionText = "Test Question", Answers = new List<Quiz_Question_Answer>(), QuizCategorieId = 1, UserId = "User5" });
            _context.Quiz_Questions.Add(new Quiz_Question { QuestionText = "Test Question", Answers = new List<Quiz_Question_Answer>(), QuizCategorieId = 1, UserId = "User5" });
            _context.Quiz_Questions.Add(new Quiz_Question { QuestionText = "Test Question", Answers = new List<Quiz_Question_Answer>(), QuizCategorieId = 1, UserId = "User5" });
            _context.Quiz_Questions.Add(new Quiz_Question { QuestionText = "Test Question", Answers = new List<Quiz_Question_Answer>(), QuizCategorieId = 1, UserId = "User5" });
            _context.SaveChanges();
        }   

        [Test, Order(1)]
        public void CreateMultiQuiz_ReturnsOk()
        {
            var userId = "1";
            var userId2 = "2";
            var categorie = 1;

            var result = _controller.CreateMultiQuizSession(new InitMultiplayerSessionDto
            {
                UserOne = userId,
                UserTwo = userId2,
                CategorieId = categorie
            });

            ClassicAssert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public void CreateMultiQuiz_ReturnsBadRequest()
        {
            var userId2 = "2";
            var categorie = 1;

            var result = _controller.CreateMultiQuizSession(new InitMultiplayerSessionDto
            {
                UserOne = null,
                UserTwo = userId2,
                CategorieId = categorie
            });

            ClassicAssert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void CreateMultiQuiz_ReturnsNotFound()
        {
            var userId = "1";
            var userId2 = "2";
            var categorie = 65892;

            var result = _controller.CreateMultiQuizSession(new InitMultiplayerSessionDto
            {
                UserOne = userId,
                UserTwo = userId2,
                CategorieId = categorie
            });

            ClassicAssert.IsInstanceOf<NotFoundObjectResult>(result);
        }

        [Test, Order(3)]
        public void GetMultiQuizzesFromUser_ReturnsOk()
        {
            var userId = "1";

            var result = _controller.GetMultiQuizzesFromUser(userId);

            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetMultiQuizzesFromUser_ReturnsOkWhenUserIdNotHaveMultiGames()
        {
            var userId = "5";

            var result = _controller.GetMultiQuizzesFromUser(userId);

            ClassicAssert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
