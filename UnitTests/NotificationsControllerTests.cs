using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Project_Quizz_API.Controllers;
using Project_Quizz_API.Data;

namespace Project_Quizz_API.UnitTests
{
    /// <summary>
    /// Notifications controller tests
    /// </summary>
    [TestFixture]
    public class NotificationsControllerTests
    {
        private NotificationsController _controller;
        private ApplicationDbContext _context;

        /// <summary>
        /// Setup method to create a new in-memory database for testing
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _controller = new NotificationsController(_context);
        }

        /// <summary>
        /// Test to check if GetOpenMultiplayerNotifications returns Ok when called with a valid userId
        /// </summary>
        [Test]
        public void GetOpenMultiplayerNotifications_ReturnsOk_WhenCalledWithValidUserId()
        {
            // Arrange
            string userId = "testUser";

            // Act
            var result = _controller.GetOpenMultiplayerNotifications(userId);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        /// <summary>
        /// Test to check if GetOpenMultiplayerNotifications returns BadRequest when called with an invalid userId
        /// </summary>
        [Test]
        public void GetOpenMultiplayerNotifications_ReturnsBadRequest_WhenCalledWithInvalidUserId()
        {
            // Arrange
            string userId = null;

            // Act
            var result = _controller.GetOpenMultiplayerNotifications(userId);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }

        /// <summary>
        /// Test to check if GetOpenSingleplayerNotifications returns Ok when called with a valid userId
        /// </summary>
        [Test]
        public void GetOpenSingleplayerNotifications_ReturnsOk_WhenCalledWithValidUserId()
        {
            // Arrange
            string userId = "testUser";

            // Act
            var result = _controller.GetOpenSingleplayerNotifications(userId);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }

        /// <summary>
        /// Test to check if GetOpenSingleplayerNotifications returns BadRequest when called with an invalid userId
        /// </summary>
        [Test]
        public void GetOpenSingleplayerNotifications_ReturnsBadRequest_WhenCalledWithInvalidUserId()
        {
            // Arrange
            string userId = null;

            // Act
            var result = _controller.GetOpenSingleplayerNotifications(userId);

            // Assert
            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }
    }
}
