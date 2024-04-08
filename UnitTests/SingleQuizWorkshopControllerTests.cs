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
    public class SingleQuizWorkshopControllerTests
    {
        private SingleQuizWorkshopController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetupInMemory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);

            _controller = new SingleQuizWorkshopController(_context);
        }

        private Single_Quiz CreateAndSaveTestQuizSession()
        {
            var testQuiz = new Single_Quiz
            {
                UserId = "sjfsdgs",
                Score = 1,
                CreateDate = DateTime.Now,
                QuizCompleted = false
            };
            _context.Single_Quizzes.Add(testQuiz);
            _context.SaveChanges();
            return testQuiz;
        }

        private void CreateAndSaveTestQuizzesForUser(string userId, int numberOfQuizzes)
        {
            Random random = new Random();
            for (int i = 0; i < numberOfQuizzes; i++)
            {
                var quiz = new Single_Quiz
                {
                    UserId = userId,
                    Score = i,
                    CreateDate = DateTime.Now,
                    QuizCategorieId = 1,
                    QuizCompleted = random.Next(2) == 1
                };
                _context.Single_Quizzes.Add(quiz);
            }
            _context.SaveChanges();
        }

        [Test]
        public void CreateSingleQuizSession_Successful_WithValidUserId()
        {
            // Arrange
            string testUserId = "validUserId";
            int categorieId = 1;

            // Act
            var result = _controller.CreateSingleQuizSession(testUserId, categorieId) as CreatedAtActionResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
        }

        [Test]
        public void CreateSingleQuizSession_Fails_WithNullUserId()
        {
            // Arrange
            string testUserId = null;
            int categorieId = 1;

            // Act
            var result = _controller.CreateSingleQuizSession(testUserId, categorieId) as BadRequestObjectResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
            ClassicAssert.AreEqual("UserId must not be null", result.Value);
        }

    }
}
