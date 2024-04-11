using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models.DTOs;
using Project_Quizz_API.Services;

namespace Project_Quizz_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuthoriziation]
    public class UserInformationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserInformationController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get user statistics of quiz sessions
        /// </summary>
        /// <param name="userId">User id from user</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserProgressInformation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserProgressInformation(string userId)
        {
            if(userId.IsNullOrEmpty())
            {
                return BadRequest();
            }

            var userInformation = _context.Quiz_Match_Overview_Users.First(x => x.UserId == userId);

            if (userInformation == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<QuizMatchOverviewUserDto>(userInformation);

            return Ok(result);
        }

        /// <summary>
        /// Get highscore data of all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetHigscroeData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetHigscroeData()
        {
            try
            {
                var result = new List<HighscoreDataDto>();
                var allUsersData = _context.Quiz_Match_Overview_Users.ToList();

                foreach (var user in allUsersData)
                {
                    var totalGames = user.TotalMultiGamesCount + user.TotalSingleGamesCount;

                    var userData = new HighscoreDataDto
                    {
                        UserId = user.UserId,
                        TotalGames = totalGames,
                        TotalMultiGames = user.TotalMultiGamesCount,
                        TotalSingleGames = user.TotalSingleGamesCount,
                        TotalPointWorth = GetPointWorth(user.TotalPoints, totalGames),
                        TotalPointWorthSingle = GetPointWorth(user.TotalPointsSingle, user.TotalSingleGamesCount),
                        TotalPointWorthMulti = GetPointWorth(user.TotalPointsMulti, user.TotalMultiGamesCount),
                    };

                    result.Add(userData);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private int GetPointWorth(int totalPoints, int playCount)
        {
            if (playCount == 0)
            {
                return 0;
            }

            var result = totalPoints / playCount;
            if (playCount < 6)
            {
                var pointWorthFactor = result / 1.5;
                result = (int)Math.Round(pointWorthFactor);
            }

            return result;
        }
    }
}
