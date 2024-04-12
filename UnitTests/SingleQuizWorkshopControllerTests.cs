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

            _context.Database.EnsureDeleted();

            _context.Database.EnsureCreated();

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
            int categorieId = 6;

            _context.Quiz_Categories.Add(new Quiz_Categorie { Id = categorieId, Name = "Test Category" });
            _context.SaveChanges();

            for (int i = 0; i < 5; i++) 
            {
                _context.Quiz_Questions.Add(new Quiz_Question
                {
                    QuizCategorieId = categorieId,
                    QuestionText = $"Test Question {i}",
                    UserId = "user1"
                });
            }
            _context.SaveChanges();

            // Act
            var result = _controller.CreateSingleQuizSession(testUserId, categorieId) as CreatedAtActionResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(StatusCodes.Status201Created, result.StatusCode);
            ClassicAssert.IsNotNull(result.Value);
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

        [Test]
        public void GetQuestionFromQuizSession_ReturnsQuestionForExistingQuiz()
        {
            // Arrange
            var existingQuiz = CreateAndSaveTestQuizSession();
            // Annehmen, dass Fragen vorhanden sind
            var question = new Quiz_Question { Id = 123, QuestionText = "Test Frage", UserId = "user1" };
            _context.Quiz_Questions.Add(question);
            _context.Single_Quiz_Attempts.Add(new Single_Quiz_Attempt { SingleQuizId = existingQuiz.Id, AskedQuestionId = question.Id });
            _context.SaveChanges();

            // Act
            var result = _controller.GetQuestionFromQuizSession(existingQuiz.Id, existingQuiz.UserId) as OkObjectResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var dto = result.Value as QuizQuestionForSingleQuizDto;
            ClassicAssert.AreEqual(question.QuestionText, dto.QuestionText);
        }

        [Test]
        public void GetResultFromSingleQuiz_ReturnsCorrectResults()
        {
            // Arrange
            var quizSession = CreateAndSaveTestQuizSession();
            quizSession.Score = 20;
            quizSession.QuizCompleted = true;

            // Act
            var result = _controller.GetResultFromSingleQuiz(quizSession.Id, quizSession.UserId) as OkObjectResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
            var dto = result.Value as ResultSingleQuizDto;
            ClassicAssert.AreEqual(20, dto.Score);
            ClassicAssert.IsTrue(dto.QuizCompleted);
        }

        [Test]
        public void UpdateSingleQuizSession_UpdatesSessionSuccessfully()
        {
            // Arrange
            var quizSession = CreateAndSaveTestQuizSession();
            var question = new Quiz_Question { Id = 456, QuestionText = "Hallo du da", UserId = "user1" };
            _context.Quiz_Questions.Add(question);
            var attempt = new Single_Quiz_Attempt { SingleQuizId = quizSession.Id, AskedQuestionId = question.Id };
            _context.Single_Quiz_Attempts.Add(attempt);
            _context.SaveChanges();

            var updateDto = new UpdateSingleQuizSessionDto
            {
                QuizId = quizSession.Id,
                QuestionId = question.Id,
                UserId = quizSession.UserId,
                GivenAnswerIds = new List<SingleQuizGivenAnswerIdsDto>()
                {
                    new SingleQuizGivenAnswerIdsDto { QuizQuestionAnswerId = 789 }
                }
            };

            // Act
            var result = _controller.UpdateSingleQuizSession(updateDto) as OkResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(StatusCodes.Status200OK, result.StatusCode);
        }

    }
}
