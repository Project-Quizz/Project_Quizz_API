using Microsoft.AspNetCore.Http;
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
    public class SingleQuizWorkshopController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SingleQuizWorkshopController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return a question for specific single quiz with teh answers
        /// </summary>
        /// <param name="quizId">The id of the quiz how needs questions</param>
        /// <param name="userId">The id of the user of the quiz</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetQuestionFromQuizSession")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetQuestionFromQuizSession(int quizId, string userId)
        {
            var singleQuizFromDb = _context.Single_Quizzes.FirstOrDefault(x => x.Id == quizId);

			List<string> validationErrors = new List<string>();
			validationErrors.AddRange(GenericValidators.CheckNullOrDefault(singleQuizFromDb));
			validationErrors.AddRange(GenericValidators.CheckNullOrDefault(quizId, "id"));
			validationErrors.AddRange(GenericValidators.CheckNullOrDefault(userId, "userId"));
			if (validationErrors.Any())
			{
				return NotFound(validationErrors);
			}

			if (singleQuizFromDb.UserId != userId)
			{
				return BadRequest("User ID mismatch: The provided user ID does not match the owner of the quiz.");
			}

            var listQuestionsFromQuiz = _context.Single_Quiz_Attempts.Where(x => x.SingleQuizId == quizId).ToList();

            var questionId = listQuestionsFromQuiz.Find(x => x.AnswerDate == null);

            var questionForQuiz = _context.Quiz_Questions.FirstOrDefault(x => x.Id == questionId.AskedQuestionId);

            var question = new QuizQuestionForSingleQuizDto
            {
                QuestionId = questionForQuiz.Id,
                QuizId = quizId,
                QuestionCount = singleQuizFromDb.QuestionCount,
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
        /// Return the result of a quiz session
        /// </summary>
        /// <param name="quizId">The quiz id of the needed result</param>
        /// <param name="userId">The user id of the quiz session how need the result</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetResultFromSingleQuiz")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetResultFromSingleQuiz(int quizId, string userId)
		{
			var quizSessionFromDb = _context.Single_Quizzes.FirstOrDefault(x => x.Id == quizId);

			List<string> validationErrors = new List<string>();
			validationErrors.AddRange(GenericValidators.CheckNullOrDefault(quizSessionFromDb));
			validationErrors.AddRange(GenericValidators.CheckNullOrDefault(quizId, "id"));
			validationErrors.AddRange(GenericValidators.CheckNullOrDefault(userId, "userId"));
			if (validationErrors.Any())
			{
				return NotFound(validationErrors);
			}

            if(!quizSessionFromDb.UserId.Equals(userId))
            {
                return BadRequest();
            }

            var quizSession = new ResultSingleQuizDto
            {
                Id = quizSessionFromDb.Id,
                Score = quizSessionFromDb.Score,
                QuizCompleted = quizSessionFromDb.QuizCompleted,
                QuestionCount = quizSessionFromDb.QuestionCount
            };

            return Ok(quizSession);

		}

        /// <summary>
        /// Overview of all singlequizzes from user
        /// </summary>
        /// <param name="userId">Id from user</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSingleQuizzesFromUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSingleQuizzesFromUser (string userId)
        {
            var result = new List<GetSingleQuizzesFromUserDto>();
            var quizzesFromPlayer = _context.Single_Quizzes.Where(x => x.UserId == userId).ToList();
            var categories = _context.Quiz_Categories.ToList();

            try
            {
                foreach(var singleSession in quizzesFromPlayer)
                {
                    var categorieId = singleSession.QuizCategorieId;

                    result.Add(new GetSingleQuizzesFromUserDto
                    {
                        QuizId = singleSession.Id,
                        QuizCreated = singleSession.CreateDate,
                        UserCompletedQuiz = singleSession.QuizCompleted,
                        Score = singleSession.Score,
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
        /// Update of an existing quiz session.
        /// </summary>
        /// <param name="updateSingleQuizSession"></param>
        /// <response code="200">When the quiz session has been successfully updated</response>
        /// <response code="202">When the quiz session is complete and all questions have been answered</response>
        /// <response code="400">If the request is invalid, for example if the data is incorrect or false</response>
        /// <response code="401">If the user tries to change answers that have already been given</response>
        [HttpPut]
        [Route("UpdateSingleQuizSession")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public IActionResult UpdateSingleQuizSession(UpdateSingleQuizSessionDto updateSingleQuizSession)
        {
            var quizSession = _context.Single_Quizzes.FirstOrDefault(x => x.Id == updateSingleQuizSession.QuizId);
			var answerIds = updateSingleQuizSession.GivenAnswerIds.Select(a => a.QuizQuestionAnswerId).ToList();
			var answersFromDb = _context.Quiz_Question_Answers.Where(answer => answerIds.Contains(answer.Id)).ToList();
			var attempt = _context.Single_Quiz_Attempts.FirstOrDefault(x => x.SingleQuizId == updateSingleQuizSession.QuizId && x.AskedQuestionId == updateSingleQuizSession.QuestionId);

			if (quizSession == null || !quizSession.UserId.Equals(updateSingleQuizSession.UserId))
            {
                return BadRequest();
            }

            if (attempt == null)
            {
                return BadRequest();
            }

            if (attempt.AnswerDate != null || quizSession.QuizCompleted == true)
            {
                return Unauthorized();
            }

			foreach (var answer in answersFromDb)
			{
				if (answer == null || answer.QuestionId != updateSingleQuizSession.QuestionId)
				{
					return BadRequest();
				}

				if (answer.IsCorrectAnswer)
				{
					quizSession.Score += 5;
				}

				var newAnswer = new Single_Given_Answer_Attepmt
				{
					SingleQuizAttemptId = attempt.Id,
					QuizQuestionAnswerId = answer.Id
				};

				_context.Single_Given_Answer_Attepmts.Add(newAnswer);
			}

			attempt.AnswerDate = DateTime.Now;
            quizSession.QuestionCount -= 1;

            _context.SaveChanges();

            if(quizSession.QuestionCount == 0)
            {
                quizSession.QuizCompleted = true;
                _context.SaveChanges();
                return Accepted();
            }

            return Ok();
        }

        /// <summary>
        /// Create a single Quiz for specific User
        /// </summary>
        /// <param name="userId">The Id from User who will create a single quiz</param>
        /// <param name="categorieId">The Id of the Categorie of the Single Quiz</param>
        /// <returns>Id of created single quiz</returns>
        [HttpPost]
        [Route("CreateSingleQuizSession")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateSingleQuizSession(string userId, int categorieId)
        {
            //Muss entfernt werden wenn Kategorien implementiert sind
            if (categorieId == 0)
            {
                categorieId = 1;
            }

            if (userId == null)
            {
                return BadRequest("UserId must not be null");
            }

            if (_context.Quiz_Categories.FirstOrDefault(x=> x.Id == categorieId) == null)
            {
                return NotFound($"Categorie with Id {categorieId} not Found");
            }

            var singleQuiz = new Single_Quiz
            {
                UserId = userId,
                Score = 0,
                QuizCategorieId = categorieId,
                Quiz_Attempts = new List<Single_Quiz_Attempt>(),
                CreateDate = DateTime.Now
            };

            var randomQuestions = _context.Quiz_Questions
                .Where(x => x.QuizCategorieId == categorieId)
                .OrderBy(x => Guid.NewGuid())
                .Take(5)
                .ToList();

            foreach (var question in randomQuestions)
            {
                singleQuiz.Quiz_Attempts.Add(new Single_Quiz_Attempt
                {
                    AskedQuestionId = question.Id,
                    AnswerDate = null
                });
            }

            singleQuiz.QuestionCount = randomQuestions.Count;

            _context.Add(singleQuiz);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetQuestionFromQuizSession), new { singleQuizId = singleQuiz.Id });
        }
    }
}
