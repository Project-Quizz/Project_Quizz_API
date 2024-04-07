using Microsoft.AspNetCore.Mvc;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models;
using Project_Quizz_API.Models.DTOs;
using Project_Quizz_API.Services;
using Project_Quizz_API.Validations;

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

		/// <summary>
		/// Returns next question for multiplayer quiz for specific user
		/// </summary>
		/// <param name="quizId">Multiplayer quiz id</param>
		/// <param name="userId">Id from user</param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetQuestionFromMultiQuizSession")]
		public IActionResult GetQuestionFromMultiQuizSession(int quizId, string userId)
		{
			var playerFromMultiQuiz = _context.Multi_Quiz_Players.FirstOrDefault(x => x.UserId == userId && x.MultiQuizId == quizId);

			List<string> validationErrors = new List<string>();
			validationErrors.AddRange(GenericValidators.CheckNullOrDefault(playerFromMultiQuiz));
			validationErrors.AddRange(GenericValidators.CheckNullOrDefault(quizId, "id"));
			validationErrors.AddRange(GenericValidators.CheckNullOrDefault(userId, "userId"));
			if (validationErrors.Any())
			{
				return NotFound(validationErrors);
			}

			if(playerFromMultiQuiz.QuestionCount == 0)
			{
				return BadRequest();
			}

			var listQuestionsFromQuiz = _context.Multi_Quiz_Attempts.Where(x => x.MultiQuizId == quizId && x.MultiQuizPlayerId == playerFromMultiQuiz.Id).ToList();
			var questionId = listQuestionsFromQuiz.Find(x => x.GivenAnswerId == null);
			var questionForQuiz = _context.Quiz_Questions.FirstOrDefault(x => x.Id == questionId.AskedQuestionId);

			var question = new QuizQuestionForMultiQuizDto
			{
				QuestionId = questionForQuiz.Id,
				QuizId = quizId,
				QuestionCount = playerFromMultiQuiz.QuestionCount,
				QuestionText = questionForQuiz.QuestionText,
				Answers = new List<QuizAnswersDto>()
			};

			var answers = _context.Quiz_Question_Answers.Where(x => x.QuestionId == question.QuestionId).ToList();

			foreach (var answer in answers)
			{
				question.Answers.Add(new QuizAnswersDto
				{
					Id = answer.Id,
					AnswerText = answer.AnswerText,
					IsCorrectAnswer = answer.IsCorrectAnswer,
				});
			}

			return Ok(question);
		}

        /// <summary>
        /// Create a multi player quiz up to two user (for now)
        /// </summary>
        /// <param name="initMultiSession"></param>
        /// <returns>Id of created multi quiz</returns>
        [HttpPost]
		[Route("CreateMultiQuizSession")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult CreateMultiQuizSession(InitMultiplayerSessionDto initMultiSession)
		{
			if (initMultiSession.UserOne == null)
			{
				return BadRequest("UserId must not be null");
			}

			if (_context.Quiz_Categories.FirstOrDefault(x => x.Id == initMultiSession.CategorieId) == null)
			{
				return NotFound($"Categorie with Id {initMultiSession.CategorieId} not Found");
			}

			List<string> userList = new List<string>
			{
                initMultiSession.UserOne,
                initMultiSession.UserTwo
            };

			var multiQuiz = new Multi_Quiz
			{
				CreateDate = DateTime.Now,
				QuizCategorieId = initMultiSession.CategorieId,
				Multi_Quiz_Players = new List<Multi_Quiz_Player>(),
				Multi_Quiz_Attempts = new List<Multi_Quiz_Attempt>()
			};

			var randomeQuestions = _context.Quiz_Questions
				.Where(x => x.QuizCategorieId == initMultiSession.CategorieId)
				.OrderBy(x => Guid.NewGuid())
				.Take(5)
				.ToList();

			foreach (var user in userList)
			{
				var newQuizPlayer = new Multi_Quiz_Player
				{
					UserId = user,
					Score = 0,
					QuestionCount = randomeQuestions.Count,
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
