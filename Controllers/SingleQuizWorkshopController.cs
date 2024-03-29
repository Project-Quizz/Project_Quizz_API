using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models;
using Project_Quizz_API.Models.DTOs;

namespace Project_Quizz_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SingleQuizWorkshopController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SingleQuizWorkshopController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a specific single quiz with the questions and answers (For the execution of a quiz)
        /// </summary>
        /// <param name="id">The single Quiz Id to be returned</param>
        /// <param name="userId">Id of the user who wants to play the quiz</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSingleQuizSession")]
        [ProducesResponseType(typeof(SingleQuizDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSingleQuizSession(int id, string userId)
        {
            Single_Quiz singleQuizFromDb = _context.Single_Quizzes.FirstOrDefault(x => x.Id == id);
            if (singleQuizFromDb == null)
            {
                return NotFound();
            }

            if (singleQuizFromDb.UserId != userId)
            {
                return BadRequest();
            }

            var singleQuiz = new SingleQuizDto
            {
                Id = singleQuizFromDb.Id,
                UserId = singleQuizFromDb.UserId,
                Score = singleQuizFromDb.Score,
                CreateDate = singleQuizFromDb.CreateDate,
                QuizCompleted = singleQuizFromDb.QuizCompleted,
                Quiz_Attempts = new List<SingleQuizAttemptDto>(),
                Question = new List<QuizQuestionDto>()
            };

            var singleQuizAttempts = _context.Single_Quiz_Attempts.Where(x => x.SingleQuizId == singleQuizFromDb.Id).ToList();
            foreach (var attempt in singleQuizAttempts)
            {
                singleQuiz.Quiz_Attempts.Add(new SingleQuizAttemptDto
                {
                    Id = attempt.Id,
                    AskedQuestionId = attempt.AskedQuestionId,
                    GivenAnswerId = attempt.GivenAnswerId,
                    AnswerDate = attempt.AnswerDate,
                });
            }

            var questionIds = singleQuizAttempts.Select(x => x.AskedQuestionId).ToHashSet();
            var questions = _context.Quiz_Questions.Where(x => questionIds.Contains(x.Id)).ToList();
            foreach (var quizQuestion in questions)
            {
                var quizQuestionDto = new QuizQuestionDto
                {
                    Id = quizQuestion.Id,
                    QuestionText = quizQuestion.QuestionText,
                    UserId = quizQuestion.UserId,
                    Answers = new List<QuizAnswersDto>()
                };

                var answersForQuestion = _context.Quiz_Question_Answers.Where(x => x.QuestionId == quizQuestion.Id).ToList();

                foreach (var answer in answersForQuestion)
                {
                    quizQuestionDto.Answers.Add(new QuizAnswersDto
                    {
                        Id = answer.Id,
                        AnswerText = answer.AnswerText,
                        IsCorrectAnswer = answer.IsCorrectAnswer,
                    });
                }

                singleQuiz.Question.Add(quizQuestionDto);
            }

            var questionCountDto = singleQuiz.Question.Count();

            singleQuiz.QuestionCount = questionCountDto;

            return Ok(singleQuiz);
        }

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
                return NotFound();
            }

            var singleQuizzesFromUser = new List<AllSingleQuizzesFromUserDto>();

            foreach (var singleQuiz in allSingleQuizzesFromDb)
            {
                var buildedSingleQuiz = new AllSingleQuizzesFromUserDto
                {
                    Id = singleQuiz.Id,
                    UserId = singleQuiz.UserId,
                    Score = singleQuiz.Score,
                    CreateDate = singleQuiz.CreateDate,
                    QuizCompleted = singleQuiz.QuizCompleted,
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
        /// <param name="updatedSingleQuizSessio">UpdateSingleQuizDto: Single_Quiz and Quiz_Attempts. It is also enough to only send Quiz_Attempts that have been modified</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateSingleQuizSession")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateSingleQuizSession(UpdateSingleQuizDto updatedSingleQuizSessio)
        {
            var singleQuizSessionFromDb = _context.Single_Quizzes.FirstOrDefault(x => x.Id == updatedSingleQuizSessio.Id);

            if(singleQuizSessionFromDb == null)
            {
                return NotFound();
            }

            singleQuizSessionFromDb.Score = updatedSingleQuizSessio.Score;
            singleQuizSessionFromDb.QuizCompleted = updatedSingleQuizSessio.QuizCompleted;

            foreach (var quizAttempt in updatedSingleQuizSessio.Quiz_Attempts)
            {
                var quizAttemptFromDb = _context.Single_Quiz_Attempts.FirstOrDefault(x => x.Id == quizAttempt.Id);
                if(quizAttemptFromDb != null)
                {
                    quizAttemptFromDb.GivenAnswerId = quizAttempt.GivenAnswerId;
                    quizAttemptFromDb.AnswerDate = quizAttempt.AnswerDate;
                }
                else
                {
                    return NotFound();
                }
            }

            _context.SaveChanges();

            return Ok();
        }
        /// <summary>
        /// Create a single Quiz for specific User
        /// </summary>
        /// <param name="userId">The Id from User who will create a single quiz</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateSingleQuizSession")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateSingleQuizSession(string userId)
        {
            if (userId == null)
            {
                return BadRequest("UserId must not be null");
            }

            var singleQuiz = new Single_Quiz
            {
                UserId = userId,
                Score = 0,
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

            _context.Add(singleQuiz);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSingleQuizSession), new { singleQuizId = singleQuiz.Id });
        }
    }
}
