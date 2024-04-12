using Microsoft.AspNetCore.Mvc;
using Project_Quizz_API.Data;
using Project_Quizz_API.Services;

namespace Project_Quizz_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuthoriziation]
    public class NotificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// To get a count of open multiplayer games
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Return int as open multiplayer games</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("GetOpenMultiplayerNotifications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOpenMultiplayerNotifications(string userId)
        { 
            try
            {
                var openMultiGames = _context.Multi_Quiz_Players.Where(x => x.UserId == userId && x.QuizComplete == false);
                var openMultiGamesCount = openMultiGames.Count();
                return Ok(new { count = openMultiGamesCount });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// To get a count of open singleplayer games
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>Returns int as open singleplayer games</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("GetOpenSingleplayerNotifications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOpenSingleplayerNotifications(string userId)
        {
            try
            {
                var openSingleplayerGames = _context.Single_Quizzes.Where(x => x.UserId == userId && x.QuizCompleted == false);
                var openSIngleplayerGamesCount = openSingleplayerGames.Count();
                return Ok(new { count = openSIngleplayerGamesCount });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
