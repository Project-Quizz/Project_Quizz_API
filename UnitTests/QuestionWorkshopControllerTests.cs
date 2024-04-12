using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Project_Quizz_API.Controllers;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models;
using Project_Quizz_API.Models.DTOs;

namespace Project_Quizz_API.UnitTests
{
    [TestFixture]
    public class QuestionWorkshopControllerTests
    {
        private QuestionWorkshopController _controller;
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

            _controller = new QuestionWorkshopController(_context);
        }

        private CreateQuizQuestionDto CreateQuizQuestionDto()
        {
            var categorie = new Quiz_Categorie
            {
                Name = "Test Kategorie",
                CreateDate = DateTime.Now

            };

            _context.Add(categorie);
            _context.SaveChanges();

            var newQuestionDto = new CreateQuizQuestionDto
            {
                QuestionText = "test",
                UserId = "fjsdKOP890KJLÖ",
                Categorie = new CreateQuizCategorieDto
                {
                    CategorieId = 1
                },
                Answers = new List<CreateQuizAnswersDto>
                {
                    new CreateQuizAnswersDto
                    {
                        AnswerText = "test",
                        IsCorrectAnswer = true
                    },
                    new CreateQuizAnswersDto
                    {
                        AnswerText = "test2",
                        IsCorrectAnswer = false
                    },
                    new CreateQuizAnswersDto
                    {
                        AnswerText = "test3",
                        IsCorrectAnswer = false
                    },
                    new CreateQuizAnswersDto
                    {
                        AnswerText = "test4",
                        IsCorrectAnswer = false
                    }
                }
            };

            return newQuestionDto;
        }

        [Test]
        public void CreateQuestion_AddQuestionWithAnswersToDb()
        {
            //Arrange
            var newQuestionDto = CreateQuizQuestionDto();

            //Act
            var result = _controller.CreateQuestion(newQuestionDto) as CreatedAtActionResult;

            //Assert
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
        }

        [Test]
        public void CreateQuestion_AddQuestionWithToFewAnswersToDb()
        {
            //Arrange
            var newQuestionDto = new CreateQuizQuestionDto
            {
                QuestionText = "test",
                UserId = "fjsdKOP890KJLÖ",
                Answers = new List<CreateQuizAnswersDto>
                {
                    new CreateQuizAnswersDto
                    {
                        AnswerText = "test",
                        IsCorrectAnswer = true
                    },
                    new CreateQuizAnswersDto
                    {
                        AnswerText = "test2",
                        IsCorrectAnswer = false
                    },
                    new CreateQuizAnswersDto
                    {
                        AnswerText = "test3",
                        IsCorrectAnswer = false
                    }
                }
            };

            //Act
            var result = _controller.CreateQuestion(newQuestionDto) as BadRequestObjectResult;

            //Assert
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void CreateQuestion_AddQuestionWithoutQuestionTextToDb()
        {
            //Arrange
            var newQuestionDto = new CreateQuizQuestionDto
            {
                QuestionText = null,
                UserId = "fjsdKOP890KJLÖ",
                Answers = new List<CreateQuizAnswersDto>
                {
                    new CreateQuizAnswersDto
                    {
                        AnswerText = "test",
                        IsCorrectAnswer = true
                    },
                    new CreateQuizAnswersDto
                    {
                        AnswerText = "test2",
                        IsCorrectAnswer = false
                    },
                    new CreateQuizAnswersDto
                    {
                        AnswerText = "test3",
                        IsCorrectAnswer = false
                    },
                    new CreateQuizAnswersDto
                    {
                        AnswerText = "test4",
                        IsCorrectAnswer = false
                    }
                }
            };

            //Act
            var result = _controller.CreateQuestion(newQuestionDto) as BadRequestObjectResult;

            //Assert
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test, Order(2)]
        public void GetQuestion_WhenQuestionExists_ReturnsOkResultWithQuestion()
        {
            // Arrange
            _controller.CreateQuestion(CreateQuizQuestionDto());
            int testQuestionId = 1;

            // Act
            var result = _controller.GetQuestion(testQuestionId) as OkObjectResult;

            // Assert
            ClassicAssert.IsNotNull(result);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            var question = result.Value as QuizQuestionDto;
            ClassicAssert.IsNotNull(question);
            Assert.That(question.Id, Is.EqualTo(testQuestionId));
        }

        [Test]
        public void GetQuestion_WhenQuestionNotExists_ReturnsNotFoundResult()
        {
            int testQuestionId = 99;

            var result = _controller.GetQuestion(testQuestionId) as NotFoundObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        [Test, Order(3)]
        public void UpdateQuestion_WhenQuestionExists_ReturnsOkObjectResult()
        {
            _controller.CreateQuestion(CreateQuizQuestionDto());
            var oldQuestionResult = _controller.GetQuestion(1) as OkObjectResult;
            var oldQuestion = oldQuestionResult.Value as QuizQuestionDto;

            var updateQuestionDto = new QuizQuestionDto()
            {
                Id = 1,
                QuestionText = "Geändert",
                UserId = "fjsdKOP890KJLÖ",
                Categorie = new QuizCategorieDto
                {
                    CategorieId = 1,
                    Name = "Test Kategorie"
                },
                Answers = new List<QuizAnswersDto>
                {
                    new QuizAnswersDto
                    {
                        Id = 1,
                        AnswerText = "test2",
                        IsCorrectAnswer = true
                    },
                    new QuizAnswersDto
                    {
                        Id = 2,
                        AnswerText = "test3",
                        IsCorrectAnswer = false
                    },
                    new QuizAnswersDto
                    {
                        Id = 3,
                        AnswerText = "test4",
                        IsCorrectAnswer = false
                    },
                    new QuizAnswersDto
                    {
                        Id = 4,
                        AnswerText = "test5",
                        IsCorrectAnswer = false
                    }
                }
            };

            var result = _controller.UpdateQuestion(updateQuestionDto) as OkObjectResult;
            var updatedQuestionResult = _controller.GetQuestion(1) as OkObjectResult;
            var updatedQuestion = updatedQuestionResult.Value as QuizQuestionDto;

            ClassicAssert.AreNotEqual(oldQuestion, updatedQuestion);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public void DeleteQuestion_WhenQuestionExists_ReturnOkObjectResult()
        {
            if(!_context.Quiz_Questions.Any())
            {
                var newQuestionDto = CreateQuizQuestionDto();
                _controller.CreateQuestion(newQuestionDto);
            }

            int testQuestionId = 1;

            var result = _controller.DeleteQuestion(testQuestionId) as OkObjectResult;

            var validateResult = _controller.GetQuestion(testQuestionId) as NotFoundObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That(validateResult.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        [Test]
        public void DeleteQuestion_WhenQuestionNotExists_ReturnNotFoundResult()
        {
            int testQuestionId = 99;

            var result = _controller.DeleteQuestion(testQuestionId) as NotFoundObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }
    }
}
