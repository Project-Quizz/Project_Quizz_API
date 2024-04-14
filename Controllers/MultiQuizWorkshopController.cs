using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models;
using Project_Quizz_API.Models.DTOs;
using Project_Quizz_API.Services;
using Project_Quizz_API.Validations;

namespace Project_Quizz_API.Controllers
{
    /// <summary>
    /// Multi quiz workshop controller. It contains all endpoints for the multiplayer quiz.
    /// </summary>
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
        /// Overview of all multiquizzes from user
        /// </summary>
        /// <param name="userId">Id from user</param>
        /// <returns>Return GetMultiQuizzesFromUserDto as list with all multiplayer games from user</returns>
		/// <response code="200">Return GetMultiQuizzesFromUserDto as list with all multiplayer games from user</response>
		/// <response code="400">If the request is invalid, for example if the data is incorrect or false</response>
        [HttpGet]
		[Route("GetMultiQuizzesFromUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetMultiQuizzesFromUser(string userId)
		{
			var result = new List<GetMultiQuizzesFromUserDto>();
			var multiQuizPlayer = _context.Multi_Quiz_Players.Where(x => x.UserId == userId).ToList();
			var categories = _context.Quiz_Categories.ToList();

			try
			{
                foreach (var multiSession in multiQuizPlayer)
                {
                    var QuizId = multiSession.MultiQuizId;
                    var categorieId = _context.Multi_Quizzes.FirstOrDefault(x => x.Id == QuizId).QuizCategorieId;
					var opponendUserId = _context.Multi_Quiz_Players.FirstOrDefault(x => x.MultiQuizId == QuizId && x.UserId != userId);


                    result.Add(new GetMultiQuizzesFromUserDto
                    {
                        MultiQuizId = QuizId,
                        QuizCreated = _context.Multi_Quizzes.FirstOrDefault(x => x.Id == QuizId).CreateDate,
                        UserCompletedQuiz = multiSession.QuizComplete,
                        Score = multiSession.Score,
						OpponentUser = opponendUserId.UserId,
                        Categorie = new QuizCategorieDto()
                        {
                            CategorieId = categorieId,
                            Name = categories.FirstOrDefault(x => x.Id == categorieId).Name
                        }
                    });
                }
            } catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok(result);
		}

        /// <summary>
        /// Get next question for multiplayer quiz for specific user
        /// </summary>
        /// <param name="quizId">Multiplayer quiz id</param>
        /// <param name="userId">Id from user</param>
        /// <returns>Return QuizQuestionForMultiQuizDto as next question when quiz ist not complete</returns>
		/// <response code="200">Return QuizQuestionForMultiQuizDto as next question when quiz ist not complete</response>
		/// <response code="400">If the request is invalid, for example if the data is incorrect or false</response>
		/// <response code="404">If the request is invalid, for example if the data is incorrect or false</response>
        [HttpGet]
		[Route("GetQuestionFromMultiQuizSession")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
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

			try
			{
                var listQuestionsFromQuiz = _context.Multi_Quiz_Attempts.Where(x => x.MultiQuizId == quizId && x.MultiQuizPlayerId == playerFromMultiQuiz.Id).ToList();
                var questionId = listQuestionsFromQuiz.Find(x => x.AnswerDate == null);
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
			catch (Exception ex)
			{
                return BadRequest(ex.Message);
            }
		}

        /// <summary>
        /// Get the Result of a multi quizz session from one specific user
        /// </summary>
        /// <param name="quizId">Id of the quiz</param>
        /// <param name="userId">Id of the user</param>
        /// <returns>Return ResultMultiQuizDto as result set of an multiplayer quiz from user</returns>
        /// <response code="200">Return ResultMultiQuizDto as result set of an multiplayer quiz from user</response>
        /// <response code="404">If the request is invalid, for example if the data is incorrect or false</response>
        [HttpGet]
        [Route("GetResultFromMultiQuiz")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetResultFromMultiQuiz(int quizId, string userId)
        {
            var quizSessionFromUser = _context.Multi_Quiz_Players.FirstOrDefault(x => x.MultiQuizId == quizId && x.UserId == userId);
            var multiQuiz = _context.Multi_Quizzes.FirstOrDefault(x => x.Id == quizId);
            var opponentQuizObj = _context.Multi_Quiz_Players.FirstOrDefault(x => x.MultiQuizId == quizId && x.UserId != userId);

            List<string> validationErrors = new List<string>();
            validationErrors.AddRange(GenericValidators.CheckNullOrDefault(quizSessionFromUser));
            validationErrors.AddRange(GenericValidators.CheckNullOrDefault(multiQuiz));
            validationErrors.AddRange(GenericValidators.CheckNullOrDefault(opponentQuizObj));
            validationErrors.AddRange(GenericValidators.CheckNullOrDefault(quizId, "id"));
            validationErrors.AddRange(GenericValidators.CheckNullOrDefault(userId, "userId"));
            if (validationErrors.Any())
            {
                return NotFound(validationErrors);
            }

            var quizSession = new ResultMultiQuizDto
            {
                Id = quizId,
                Score = quizSessionFromUser.Score,
                QuizCompleted = quizSessionFromUser.QuizComplete,
                QuestionCount = quizSessionFromUser.QuestionCount,
                MultiQuizComplete = multiQuiz.QuizCompleted,
                Opponent = new OpponentDto
                {
                    UserId = opponentQuizObj.UserId,
                    Score = opponentQuizObj.Score,
                    QuizComplete = opponentQuizObj.QuizComplete
                }
            };

            return Ok(quizSession);


        }

        /// <summary>
        /// Update of an existing multiplayer quiz session.
        /// </summary>
        /// <param name="updateMultiQuizSession">UpdateMultiQuizSessionDto</param>
        /// <returns>Returns only StatusCode</returns>
        /// <response code="200">When the quiz session has been successfully updated</response>
        /// <response code="202">When the quiz session is complete and all questions have been answered</response>
        /// <response code="400">If the request is invalid, for example if the data is incorrect or false</response>
        /// <response code="401">If the user tries to change answers that have already been given</response>
        [HttpPut]
		[Route("UpdateMultiQuizSession")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status202Accepted)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public IActionResult UpdateMultiQuizSession(UpdateMultiQuizSessionDto updateMultiQuizSession)
		{
			var quizSessionFromPlayer = _context.Multi_Quiz_Players.FirstOrDefault(x => x.UserId == updateMultiQuizSession.UserId && x.MultiQuizId == updateMultiQuizSession.QuizId);
			var answerIds = updateMultiQuizSession.GivenAnswerIds.Select(a => a.QuizQuestionAnswerId).ToList();
			var answersFromDb = _context.Quiz_Question_Answers.Where(answer => answerIds.Contains(answer.Id)).ToList();
			var attempt = _context.Multi_Quiz_Attempts.FirstOrDefault(x => x.MultiQuizId == updateMultiQuizSession.QuizId && x.AskedQuestionId == updateMultiQuizSession.QuestionId && x.MultiQuizPlayerId == quizSessionFromPlayer.Id);
			var multiQuiz = _context.Multi_Quizzes.FirstOrDefault(x => x.Id == updateMultiQuizSession.QuizId);
            var matchOverview = _context.Quiz_Match_Overview_Users.FirstOrDefault(x => x.UserId == updateMultiQuizSession.UserId);

            if (quizSessionFromPlayer == null || !quizSessionFromPlayer.UserId.Equals(updateMultiQuizSession.UserId))
			{
				return BadRequest();
			}

			if (attempt == null || multiQuiz == null)
			{
				return BadRequest();
			}

			if (attempt.AnswerDate != null || quizSessionFromPlayer.QuizComplete == true)
			{
				return Unauthorized();
			}

            if (matchOverview == null)
            {
                var newMatchOverview = new Quiz_Match_Overview_User
                {
                    UserId = updateMultiQuizSession.UserId,
                };

                _context.Quiz_Match_Overview_Users.Add(newMatchOverview);
                matchOverview = newMatchOverview;
                _context.SaveChanges();
            }

            var correctAnswerCount = _context.Quiz_Question_Answers.Where(x => x.QuestionId == updateMultiQuizSession.QuestionId && x.IsCorrectAnswer == true).Count();
            var correctAnswerFromUserCount = 0;

            foreach (var answer in answersFromDb)
			{
				if (answer == null || answer.QuestionId != updateMultiQuizSession.QuestionId)
				{
					return BadRequest();
				}

                if (answer.IsCorrectAnswer)
                {
                    correctAnswerFromUserCount++;
                }

                var newAnswer = new Multi_Given_Answer_Attempt
				{
					MultiQuizAttemptId = attempt.Id,
					QuizQuestionAnswerId = answer.Id
				};

				_context.Multi_Given_Answer_Attempts.Add(newAnswer);
			}

            if (correctAnswerCount == correctAnswerFromUserCount)
            {
                quizSessionFromPlayer.Score += 5;
            }

            attempt.AnswerDate = DateTime.Now;
			quizSessionFromPlayer.QuestionCount -= 1;

			_context.SaveChanges();

			if (quizSessionFromPlayer.QuestionCount == 0)
			{
				quizSessionFromPlayer.QuizComplete = true;
				var allPlayersFromQuiz = _context.Multi_Quiz_Players.Where(x => x.MultiQuizId == updateMultiQuizSession.QuizId).ToList();
				if(allPlayersFromQuiz.All(x => x.QuizComplete))
				{
					multiQuiz.QuizCompleted = true;
				}
                matchOverview.TotalPointsMulti += quizSessionFromPlayer.Score;
                matchOverview.TotalPoints += quizSessionFromPlayer.Score;
                matchOverview.TotalMultiGamesCount++;

                if (quizSessionFromPlayer.Score >= 20)
                {
					matchOverview.MultiGoldCount ++;
                }
                else if (quizSessionFromPlayer.Score >= 10 && quizSessionFromPlayer.Score < 20)
                {
                    matchOverview.MultiSilverCount++;
                }
                else
                {
                    matchOverview.MultiBronzeCount++;
                }

                _context.SaveChanges();
				return Accepted();
			}

			return Ok();
		}

        /// <summary>
        /// Create a multi player quiz up to two user (for now)
        /// </summary>
        /// <param name="initMultiSession">InitMultiplayerSessionDto</param>
        /// <returns>Id of created multi quiz</returns>
        /// <response code="201">Return Id of created multi quiz</response>
        /// <response code="400">If the request is invalid, for example if the data is incorrect or false</response>
        /// <response code="404">If the request is invalid, for example if the data is incorrect or false</response>
        [HttpPost]
		[Route("CreateMultiQuizSession")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult CreateMultiQuizSession(InitMultiplayerSessionDto initMultiSession)
		{
			if (initMultiSession.UserOne == null || initMultiSession.UserTwo == null)
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
