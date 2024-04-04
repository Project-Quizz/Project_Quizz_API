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

        //[Test]
        //public void UpdateSingleQuizSession_Successful_WithValidData()
        //{
        //    // Arrange
        //    var testQuizSession = CreateAndSaveTestQuizSession();
        //    var updatedQuizSessionDto = new UpdateSingleQuizDto
        //    {
        //        Id = testQuizSession.Id,
        //        Score = 10,
        //        QuizCompleted = true,
        //        Quiz_Attempts = new List<UpdateSingleQuizAttemptDto>()
        //    };

        //    // Act
        //    var result = _controller.UpdateSingleQuizSession(updatedQuizSessionDto) as OkResult;
        //    var actual = _context.Single_Quizzes.SingleOrDefault(x => x.Id == testQuizSession.Id);

        //    // Assert
        //    ClassicAssert.IsNotNull(result);
        //    ClassicAssert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        //    ClassicAssert.AreEqual(actual.Score, updatedQuizSessionDto.Score);
        //    // Sie können auch überprüfen, ob die Änderungen in der Datenbank korrekt vorgenommen wurden.
        //}

        //[Test]
        //public void UpdateSingleQuizSession_Fails_WhenQuizDoesNotExist()
        //{
        //    // Arrange
        //    var updatedQuizSessionDto = new UpdateSingleQuizDto
        //    {
        //        Id = 99,
        //        Score = 10,
        //        QuizCompleted = true,
        //    };

        //    // Act
        //    var result = _controller.UpdateSingleQuizSession(updatedQuizSessionDto) as NotFoundObjectResult;

        //    // Assert
        //    ClassicAssert.IsNotNull(result);
        //    ClassicAssert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        //}

        [Test]
        public void GetAllSingleQuizzesFromUser_ReturnsQuizzes_WhenUserHasQuizzes()
        {
            // Arrange
            string testUserId = "existingUserId";
            CreateAndSaveTestQuizzesForUser(testUserId, 3);

            // Act
            var result = _controller.GetAllSingleQuizzesFromUser(testUserId) as OkObjectResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var quizzes = result.Value as List<AllSingleQuizzesFromUserDto>;
            ClassicAssert.IsNotNull(quizzes);
            ClassicAssert.AreEqual(3, quizzes.Count);
        }

        [Test]
        public void GetAllSingleQuizzesFromUser_ReturnsNotFound_WhenUserHasNoQuizzes()
        {
            // Arrange
            string testUserId = "nonExistingUserId";

            // Act
            var result = _controller.GetAllSingleQuizzesFromUser(testUserId) as NotFoundObjectResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
            ClassicAssert.AreEqual($"No quizzes found for the user with ID '{testUserId}'.", result.Value);
        }

    }
}
