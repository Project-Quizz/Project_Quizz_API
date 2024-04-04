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

        [HttpGet]
        [Route("GetQuestionFromQuizSession")]
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

            var questionId = listQuestionsFromQuiz.Find(x => x.GivenAnswerId == null);

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
        /// Returns a specific single quiz with the questions and answers (For the execution of a quiz)
        /// </summary>
        /// <param name="id">The single Quiz Id to be returned</param>
        /// <param name="userId">Id of the user who wants to play the quiz</param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("GetSingleQuizSession")]
        //[ProducesResponseType(typeof(SingleQuizDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult GetSingleQuizSession(int id, string userId)
        //{
        //    var singleQuizFromDb = _context.Single_Quizzes.FirstOrDefault(x => x.Id == id);

        //    List<string> validationErrors = new List<string>();
        //    validationErrors.AddRange(GenericValidators.CheckNullOrDefault(singleQuizFromDb));
        //    validationErrors.AddRange(GenericValidators.CheckNullOrDefault(id, "id"));
        //    validationErrors.AddRange(GenericValidators.CheckNullOrDefault(userId, "userId"));
        //    if (validationErrors.Any())
        //    {
        //        return NotFound(validationErrors);
        //    }

        //    if (singleQuizFromDb.UserId != userId)
        //    {
        //        return BadRequest("User ID mismatch: The provided user ID does not match the owner of the quiz.");
        //    }

        //    var singleQuiz = new SingleQuizDto
        //    {
        //        Id = singleQuizFromDb.Id,
        //        UserId = singleQuizFromDb.UserId,
        //        Score = singleQuizFromDb.Score,
        //        CreateDate = singleQuizFromDb.CreateDate,
        //        QuizCompleted = singleQuizFromDb.QuizCompleted,
        //        Quiz_Attempts = new List<SingleQuizAttemptDto>(),
        //        Question = new List<QuizQuestionDto>()
        //    };

        //    var singleQuizAttempts = _context.Single_Quiz_Attempts.Where(x => x.SingleQuizId == singleQuizFromDb.Id).ToList();
        //    foreach (var attempt in singleQuizAttempts)
        //    {
        //        singleQuiz.Quiz_Attempts.Add(new SingleQuizAttemptDto
        //        {
        //            Id = attempt.Id,
        //            AskedQuestionId = attempt.AskedQuestionId,
        //            GivenAnswerId = attempt.GivenAnswerId,
        //            AnswerDate = attempt.AnswerDate,
        //        });
        //    }

        //    var questionIds = singleQuizAttempts.Select(x => x.AskedQuestionId).ToHashSet();
        //    var questions = _context.Quiz_Questions.Where(x => questionIds.Contains(x.Id)).ToList();
        //    foreach (var quizQuestion in questions)
        //    {
        //        var quizQuestionDto = new QuizQuestionDto
        //        {
        //            Id = quizQuestion.Id,
        //            QuestionText = quizQuestion.QuestionText,
        //            UserId = quizQuestion.UserId,
        //            Answers = new List<QuizAnswersDto>()
        //        };

        //        var answersForQuestion = _context.Quiz_Question_Answers.Where(x => x.QuestionId == quizQuestion.Id).ToList();

        //        foreach (var answer in answersForQuestion)
        //        {
        //            quizQuestionDto.Answers.Add(new QuizAnswersDto
        //            {
        //                Id = answer.Id,
        //                AnswerText = answer.AnswerText,
        //                IsCorrectAnswer = answer.IsCorrectAnswer,
        //            });
        //        }

        //        singleQuiz.Question.Add(quizQuestionDto);
        //    }

        //    var questionCountDto = singleQuiz.Question.Count();

        //    singleQuiz.QuestionCount = questionCountDto;

        //    return Ok(singleQuiz);
        //}

        /// <summary>
        /// Returns all single Quizzes from specific User
        /// </summary>
        /// <param name="userId">The userId from the specific User</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllSingleQuizzesFromUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllSingleQuizzesFromUser(string userId)
        {
            var allSingleQuizzesFromDb = _context.Single_Quizzes.Where(x => x.UserId == userId).ToList();
            if (allSingleQuizzesFromDb.Count == 0)
            {
                return NotFound($"No quizzes found for the user with ID '{userId}'.");
            }

            var singleQuizzesFromUser = new List<AllSingleQuizzesFromUserDto>();

            foreach (var singleQuiz in allSingleQuizzesFromDb)
            {
                var categorie = _context.Quiz_Categories.FirstOrDefault(x => x.Id == singleQuiz.QuizCategorieId);

                if (categorie == null)
                {
                    return NotFound($"Categorie for Single Quiz with Id {singleQuiz.Id} not found. Categorie Id {singleQuiz.QuizCategorieId}");
                }

                var buildedSingleQuiz = new AllSingleQuizzesFromUserDto
                {
                    Id = singleQuiz.Id,
                    UserId = singleQuiz.UserId,
                    Score = singleQuiz.Score,
                    CreateDate = singleQuiz.CreateDate,
                    QuizCompleted = singleQuiz.QuizCompleted,
                    Categorie = new AllSingleQuizzesCategorieDto
                    {
                        CategorieId = categorie.Id,
                        Name = categorie.Name
                    },
                    Quiz_Attempts = new List<AllSingleQuizzesAttemptDto>()
                };

                var singleQuizAttempts = _context.Single_Quiz_Attempts.Where(x => x.SingleQuizId == singleQuiz.Id);
                foreach (var attempt in singleQuizAttempts)
                {
                    buildedSingleQuiz.Quiz_Attempts.Add(new AllSingleQuizzesAttemptDto
                    {
                        Id = attempt.Id,
                        AskedQuestionId = attempt.AskedQuestionId,
                        GivenAnswerId = attempt.GivenAnswerId,
                        AnswerDate = attempt.AnswerDate,
                    });
                }

                singleQuizzesFromUser.Add(buildedSingleQuiz);
            }

            return Ok(singleQuizzesFromUser);
        }

		/// <summary>
		/// Update of an existing quiz session. It is also enough to only send Quiz_Attempts that have been modified
		/// </summary>
		/// <param name="quizId"></param>
		/// <param name="questionId"></param>
		/// <param name="answerFromUserId"></param>
		/// <param name="userId"></param>
		/// <param name="updatedSingleQuizSessio">UpdateSingleQuizDto: Single_Quiz and Quiz_Attempts. It is also enough to only send Quiz_Attempts that have been modified</param>
		/// <returns></returns>
		[HttpPut]
        [Route("UpdateSingleQuizSession")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateSingleQuizSession(int quizId, int questionId, int answerFromUserId, string userId)
        {
            var quizSession = _context.Single_Quizzes.FirstOrDefault(x => x.Id == quizId);

            if (quizSession == null || !quizSession.UserId.Equals(userId))
            {
                return BadRequest();
            }

            var answerFromDb = _context.Quiz_Question_Answers.FirstOrDefault(x => x.Id == answerFromUserId);

            if (answerFromDb == null || answerFromDb.QuestionId != questionId)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Create a single Quiz for specific User
        /// </summary>
        /// <param name="userId">The Id from User who will create a single quiz</param>
        /// <param name="categorieId">The Id of the Categorie of the Single Quiz</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateSingleQuizSession")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateSingleQuizSession(string userId, int categorieId)
        {
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

            var randomQuestions = _context.Quiz_Questions.OrderBy(q => Guid.NewGuid()).Take(5).ToList();

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
