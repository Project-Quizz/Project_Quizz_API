﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models;
using Project_Quizz_API.Models.DTOs;
using Project_Quizz_API.Services;
using Project_Quizz_API.Validations;

namespace Project_Quizz_API.Controllers
{
    /// <summary>
    /// Single quiz workshop controller. It contains the endpoints for the single quiz workshop.
    /// </summary>
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
        /// To get a question from a quiz session.
        /// </summary>
        /// <param name="quizId">The ID of the quiz that requires the questions.</param>
        /// <param name="userId">The ID of the user of the quiz.</param>
        /// <returns>An IActionResult that contains either a BadRequest, NotFound or Ok response.
        /// In the case of an OK response, a DTO is returned with the question and its answers.</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="204"></response>
        [HttpGet]
        [Route("GetQuestionFromQuizSession")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// To get the result of a single quiz from specific user.
        /// </summary>
        /// <param name="quizId">The quiz id of the needed result</param>
        /// <param name="userId">The user id of the quiz session how need the result</param>
        /// <returns>The result set with Score, the QuestionCount and the information if quiz is complete.</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [HttpGet]
        [Route("GetResultFromSingleQuiz")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// To get all single quizzes from a specific user.
        /// </summary>
        /// <param name="userId">Id from user</param>
        /// <returns>Return the set of all single quizzes from a user</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("GetSingleQuizzesFromUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetSingleQuizzesFromUser (string userId)
        {
            var result = new List<GetSingleQuizzesFromUserDto>();
            var categories = _context.Quiz_Categories.ToList();

            try
            {
                var quizzesFromPlayer = _context.Single_Quizzes.Where(x => x.UserId == userId).ToList();
                foreach (var singleSession in quizzesFromPlayer)
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
            var matchOverview = _context.Quiz_Match_Overview_Users.FirstOrDefault(x => x.UserId == updateSingleQuizSession.UserId);

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

            if(matchOverview == null)
            {
                var newMatchOverview = new Quiz_Match_Overview_User
                {
                    UserId = updateSingleQuizSession.UserId,
                };

                _context.Quiz_Match_Overview_Users.Add(newMatchOverview);
                matchOverview = newMatchOverview;
                _context.SaveChanges();
            }

            var correctAnswerCount = _context.Quiz_Question_Answers.Where(x => x.QuestionId == updateSingleQuizSession.QuestionId && x.IsCorrectAnswer == true).Count();
            var givenAnswerCount = updateSingleQuizSession.GivenAnswerIds.Count();   
            var correctAnswerFromUserCount = 0;

			foreach (var answer in answersFromDb)
			{
				if (answer == null || answer.QuestionId != updateSingleQuizSession.QuestionId)
				{
					return BadRequest();
				}

				if (answer.IsCorrectAnswer)
				{
                    correctAnswerFromUserCount++;
				}

				var newAnswer = new Single_Given_Answer_Attepmt
				{
					SingleQuizAttemptId = attempt.Id,
					QuizQuestionAnswerId = answer.Id
				};

				_context.Single_Given_Answer_Attepmts.Add(newAnswer);
			}

            if (correctAnswerCount == givenAnswerCount && correctAnswerCount == correctAnswerFromUserCount)
            {
                quizSession.Score += 5;
            }

			attempt.AnswerDate = DateTime.Now;
            quizSession.QuestionCount -= 1;

            _context.SaveChanges();

            if(quizSession.QuestionCount == 0)
            {
                quizSession.QuizCompleted = true;
                matchOverview.TotalPointsSingle += quizSession.Score;
                matchOverview.TotalPoints += quizSession.Score;
                matchOverview.TotalSingleGamesCount++;

                if(quizSession.Score >= 20)
                {
                    matchOverview.SingleGoldCount++;
                }
                else if (quizSession.Score >= 10 && quizSession.Score <20)
                {
                    matchOverview.SingleSilverCount++;
                } 
                else
                {
                    matchOverview.SingleBronzeCount++;
                }

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
        /// <response code="201"></response>
        /// <response code="400"></response>
        [HttpPost]
        [Route("CreateSingleQuizSession")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateSingleQuizSession(string userId, int categorieId)
        {
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
