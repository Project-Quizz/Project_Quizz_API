using Microsoft.AspNetCore.Mvc;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models;
using Project_Quizz_API.Services;

namespace Project_Quizz_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ApiKeyAuthoriziation]
	public class MultiQuizWorkshopController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

        public MultiQuizWorkshopController(ApplicationDbContext context)
        {
			_context = context;            
        }

		[HttpGet]
		[Route("GetQuestionFromMultiQuizSession")]
		public IActionResult GetQuestionFromMultiQuizSession()
		{
			return Ok();
		}

		/// <summary>
		/// Create a multi player quiz up to two user (for now)
		/// </summary>
		/// <param name="userOne">User id from initiate user</param>
		/// <param name="userTwo">Iser id from challenged user</param>
		/// <param name="categorieId">Id of the categorie of the multi quiz</param>
		/// <returns>Id of created multi quiz</returns>
		[HttpPost]
		[Route("CreateMultiQuizSession")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult CreateMultiQuizSession(string userOne, string userTwo, int categorieId)
		{
			//Muss entfernt werden wenn Kategorien implementiert sind
			if (categorieId == 0)
			{
				categorieId = 1;
			}

			if (userOne == null || userOne == null)
			{
				return BadRequest("UserId must not be null");
			}

			if (_context.Quiz_Categories.FirstOrDefault(x => x.Id == categorieId) == null)
			{
				return NotFound($"Categorie with Id {categorieId} not Found");
			}

			List<string> userList = new List<string>
			{
				userOne,
				userTwo
			};

			var multiQuiz = new Multi_Quiz
			{
				CreateDate = DateTime.Now,
				QuizCategorieId = categorieId,
				Multi_Quiz_Players = new List<Multi_Quiz_Player>(),
				Multi_Quiz_Attempts = new List<Multi_Quiz_Attempt>()
			};

			var randomeQuestions = _context.Quiz_Questions
				.Where(x => x.QuizCategorieId == categorieId)
				.OrderBy(x => Guid.NewGuid())
				.Take(5)
				.ToList();

			foreach (var user in userList)
			{
				var newQuizPlayer = new Multi_Quiz_Player
				{
					UserId = user,
					Score = 0,
					Multi_Quiz_Attempts = new List<Multi_Quiz_Attempt>()
				};

				foreach (var question in randomeQuestions)
				{
					var attemptObj = new Multi_Quiz_Attempt
					{
						AskedQuestionId = question.Id
					};
					multiQuiz.Multi_Quiz_Attempts.Add(attemptObj);
					newQuizPlayer.Multi_Quiz_Attempts.Add(attemptObj);
				}

				multiQuiz.Multi_Quiz_Players.Add(newQuizPlayer);
			}

			multiQuiz.QuestionCount = randomeQuestions.Count;

			_context.Add(multiQuiz);
			_context.SaveChanges();

			return CreatedAtAction(nameof(GetQuestionFromMultiQuizSession), new { multiQuizId = multiQuiz.Id });
		}
	}
}
