using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models;
using Project_Quizz_API.Models.DTOs;
using Project_Quizz_API.Services;
using Project_Quizz_API.Validations;

namespace Project_Quizz_API.Controllers
{
    /// <summary>
    /// Question workshop controller. It contains all endpoints for creating, updating, deleting and getting questions and feedbacks.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuthoriziation]
    public class QuestionWorkshopController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public QuestionWorkshopController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get Quiz_Question (without question feedbacks)
        /// </summary>
        /// <param name="id">Id from Quiz_Qestion</param>
        /// <returns>
        /// Return QuizQuestionDto with all QuizAnswersDto (List)
        /// </returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [HttpGet]
        [Route("GetQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetQuestion(int id)
        {
            // Check if id is null or 0
            var validationErrors = GenericValidators.CheckNullOrDefault(id, "id");
            if (validationErrors.Any())
            {
                return NotFound(validationErrors);
            }

            // Check if question exist in database
            Quiz_Question questionFromDb = _context.Quiz_Questions.SingleOrDefault(x => x.Id == id);
            if (questionFromDb == null)
            {
                return NotFound($"Question with Id {id} not found");
            }

            // Get categorie from question
            try
            {
                var categorie = _context.Quiz_Categories.FirstOrDefault(x => x.Id == questionFromDb.QuizCategorieId);

                var question = new QuizQuestionDto
                {
                    Id = questionFromDb.Id,
                    QuestionText = questionFromDb.QuestionText,
                    UserId = questionFromDb.UserId,
                    Categorie = new QuizCategorieDto
                    {
                        CategorieId = categorie.Id,
                        Name = categorie.Name
                    },
                    Answers = new List<QuizAnswersDto>()
                };

                var answers = _context.Quiz_Question_Answers.Where(x => x.QuestionId == id).ToList();
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
            };
        }

        /// <summary>
        /// Get all feedbacks from specific question
        /// </summary>
        /// <param name="questionId">Question id</param>
        /// <returns>Return GetQuizQuestionFeedbackDtowith question id and feedback</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("GetQuestionFeedbacks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetQuestionFeedbacks(int questionId)
        {
            // Check if questionId is 0 or not exist in database
            if (questionId == 0 
                || _context.Quiz_Questions.FirstOrDefault(x => x.Id == questionId) == null)
            {
                return BadRequest();
            }

            try
            {
                // Get all feedbacks for specific question
                var feedbacksForQuestion = _context.Quiz_Question_Feedbacks.Where(x => x.QuestionId == questionId).ToList();
                var feedbackList = new List<GetQuizQuestionFeedbackDto>();

                foreach (var feedback in feedbacksForQuestion)
                {
                    feedbackList.Add(new GetQuizQuestionFeedbackDto
                    {
                        QuestionId = questionId,
                        Feedback = feedback.Feedback,
                        UserId = feedback.UserId,
                        CreateDate = feedback.CreateDate
                    });
                }

                return Ok(feedbackList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all created questions from user as list
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Returns List of QuestionFromUserDto  with all questions created by the given user</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet]
        [Route("GetAllQuestionsFromUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllQuestionsFromUser(string userId)
        {
            try
            {
                var questionsFromDb = _context.Quiz_Questions.Where(x => x.UserId == userId).ToList();
                var listOfAllQuestions = new List<QuestionFromUserDto>();

                foreach (var question in questionsFromDb)
                {
                    var categorie = _context.Quiz_Categories.FirstOrDefault(x => x.Id == question.QuizCategorieId);
                    var feedbackCount = _context.Quiz_Question_Feedbacks.Where(x => x.QuestionId == question.Id).Count();

                    listOfAllQuestions.Add(new QuestionFromUserDto
                    {
                        QuestionId = question.Id,
                        QuestionText = question.QuestionText,
                        Categorie = new QuizCategorieDto
                        {
                            CategorieId = question.QuizCategorieId,
                            Name = categorie.Name,
                        },
                        FeedbackCount = feedbackCount
                    });
                }

                return Ok(listOfAllQuestions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create a question with the given parameters. It's essential to provide exactly four answers. 
        /// </summary>
        /// <param name="questionDto">The data transfer object containing the question and its answers.</param>
        /// <returns>Return the id of created question</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [HttpPost]
        [Route("CreateQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateQuestion(CreateQuizQuestionDto questionDto)
        {
            var validationErrors = QuestionWorkshopControllerValidator.ValidateQuestion(questionDto);
            if (validationErrors.Any())
            {
                return BadRequest(validationErrors);
            }

            if (_context.Quiz_Categories.FirstOrDefault(x => x.Id == questionDto.Categorie.CategorieId) == null)
            {
                return NotFound("Given categorie not found");
            }

            try
            {
                var question = new Quiz_Question
                {
                    QuestionText = questionDto.QuestionText,
                    UserId = questionDto.UserId,
                    QuizCategorieId = questionDto.Categorie.CategorieId,
                    Answers = new List<Quiz_Question_Answer>()
                };

                foreach (var answerDto in questionDto.Answers)
                {
                    question.Answers.Add(new Quiz_Question_Answer
                    {
                        AnswerText = answerDto.AnswerText,
                        IsCorrectAnswer = answerDto.IsCorrectAnswer
                    });
                }

                _context.Quiz_Questions.Add(question);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetQuestion), new { questionId = question.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create feeedback for spezific question
        /// </summary>
        /// <param name="givenFeedback">Dto object wit QuestionId the Feedback and UserId</param>
        /// <returns>Return only StatusCode</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        [HttpPost]
        [Route("CreateFeedbackForQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult CreateFeedbackForQuestion(QuizQuestionFeedbackDto givenFeedback)
        {
            if ( givenFeedback == null 
                || givenFeedback.Feedback.IsNullOrEmpty()
                || givenFeedback.UserId.IsNullOrEmpty())
            {
                return BadRequest();
            }

            var questionFromDb = _context.Quiz_Questions.FirstOrDefault(x => x.Id == givenFeedback.QuestionId);
            if (questionFromDb == null)
            {
                return NotFound();  
            }
            
            try
            {
				var newFeedback = new Quiz_Question_Feedback
				{
					QuestionId = questionFromDb.Id,
					Feedback = givenFeedback.Feedback,
					UserId = givenFeedback.UserId,
					CreateDate = DateTime.Now
				};

				_context.Quiz_Question_Feedbacks.Add(newFeedback);
				_context.SaveChanges();

				return Ok();
			}
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Updates an existing quiz question and its answers based on the provided data (without question feedback)
        /// </summary>
        /// <param name="questionDto">The data transfer object containing the updated information for the quiz question and its answers. </param>
        /// <returns>Return StatusCode ok</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        /// <response code="404"></response>
        /// <response code="401"></response>
        [HttpPut]
        [Route("UpdateQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateQuestion(QuizQuestionDto questionDto)
        {
            var questionFromDb = _context.Quiz_Questions.Include(a => a.Answers).FirstOrDefault(x => x.Id == questionDto.Id);

            var validationErrors = GenericValidators.CheckIfObjectExist(questionFromDb, nameof(Quiz_Question));
            if (validationErrors.Any())
            {
                return NotFound(validationErrors);
            }

            validationErrors = QuestionWorkshopControllerValidator.ValidateQuestion(questionDto);
            if (validationErrors.Any())
            {
                return BadRequest(validationErrors);
            }

            if (questionDto.UserId != questionFromDb.UserId)
            {
                return Unauthorized();
            }

            if (_context.Quiz_Categories.FirstOrDefault(x => x.Id == questionDto.Categorie.CategorieId) == null)
            {
                return NotFound("Given categorie not found");
            }

            try
            {
                questionFromDb.QuestionText = questionDto.QuestionText;
                questionFromDb.UserId = questionDto.UserId;
                questionFromDb.QuizCategorieId = questionDto.Categorie.CategorieId;

                foreach (var answer in questionDto.Answers)
                {
                    var answerFromDb = questionFromDb.Answers.FirstOrDefault(a => a.Id == answer.Id);
                    if (answerFromDb != null)
                    {
                        answerFromDb.AnswerText = answer.AnswerText;
                        answerFromDb.IsCorrectAnswer = answer.IsCorrectAnswer;
                    }
                    else
                    {
                        return NotFound($"Answer with Id {answer.Id} not found");
                    }
                }

                _context.SaveChanges();
                return Ok("Update Quiz Question with Answers successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Deletes a quiz question and its associated answers from the database.
        /// </summary>
        /// <param name="questionId">The ID of the quiz question to be deleted.</param>
        /// <returns>Return only StatusCode</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpDelete]
        [Route("DeleteQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteQuestion(int questionId)
        {
            try
            {
                var questionFromDb = _context.Quiz_Questions.Include(a => a.Answers).FirstOrDefault(x => x.Id == questionId);
                var validateErrors = GenericValidators.CheckIfObjectExist(questionFromDb, nameof(Quiz_Question));
                if (validateErrors.Any())
                {
                    return NotFound(validateErrors);
                }

                _context.Remove(questionFromDb);
                _context.SaveChanges();

                return Ok("Question deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
