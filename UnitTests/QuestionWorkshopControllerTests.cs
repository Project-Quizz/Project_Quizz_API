using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Project_Quizz_API.Controllers;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models;
using Project_Quizz_API.Models.DTOs;

namespace Project_Quizz_API.UnitTests
{
    [TestFixture]
    public class QuestionWorkshopControllerTests
    {
        private Mock<ApplicationDbContext> _mockContext;
        private QuestionWorkshopController _controller;
        private Mock<DbSet<Quiz_Question>> _mockSet;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _mockContext = new Mock<ApplicationDbContext>(options);
            _mockSet = new Mock<DbSet<Quiz_Question>>();
            _mockContext.Setup(m => m.QuizQuestions).Returns(_mockSet.Object);

            _controller = new QuestionWorkshopController(_mockContext.Object);
        }

        [Test]
        public void CreateQuestion_AddQuestionWithAnswersToDb()
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
                    },
                    new CreateQuizAnswersDto
                    {
                        AnswerText = "test4",
                        IsCorrectAnswer = false
                    }
                }
            };

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
    }
}
