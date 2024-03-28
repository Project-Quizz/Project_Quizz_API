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
        /// Returns a specific single quiz
        /// </summary>
        /// <param name="id">The single Quiz Id to be returned</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSingleQuiz")]
        [ProducesResponseType(typeof(SingleQuizDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetSingleQuiz(int id)
        {
            Single_Quiz singleQuizFromDb = _context.Single_Quizzes.FirstOrDefault(x => x.Id == id);
            if (singleQuizFromDb == null)
            {
                return NotFound();
            }

            var singleQuiz = new SingleQuizDto
            {
                Id = singleQuizFromDb.Id,
                UserId = singleQuizFromDb.UserId,
                Score = singleQuizFromDb.Score,
                CreateDate = singleQuizFromDb.CreateDate,
                QuizCompleted = singleQuizFromDb.QuizCompleted,
                Quiz_Attempts = new List<SingleQuizAttemptDto>()
            };

            var singleQuizAttempts = _context.Single_Quiz_Attempts.Where(x => x.SingleQuizId == singleQuizFromDb.Id);
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

            return Ok(singleQuiz);
        }

        /// <summary>
        /// Returns all single Quizzes from specific User
        /// </summary>
        /// <param name="userId">The userId from the specific User</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllSingleQuizzesFromUser")]
        public IActionResult GetAllSingleQuizzesFromUser(string userId)
        {
            var allSingleQuizzesFromDb = _context.Single_Quizzes.Where(x => x.UserId == userId).ToList();
            if (allSingleQuizzesFromDb.Count == 0)
            {
                return NotFound();
            }

            var singleQuizzesFromUser = new List<SingleQuizDto>();

            foreach (var singleQuiz in allSingleQuizzesFromDb)
            {
                var buildedSingleQuiz = new SingleQuizDto
                {
                    Id = singleQuiz.Id,
                    UserId = singleQuiz.UserId,
                    Score = singleQuiz.Score,
                    CreateDate = singleQuiz.CreateDate,
                    QuizCompleted = singleQuiz.QuizCompleted,
                    Quiz_Attempts = new List<SingleQuizAttemptDto>()
                };

                var singleQuizAttempts = _context.Single_Quiz_Attempts.Where(x => x.SingleQuizId == singleQuiz.Id);
                foreach (var attempt in singleQuizAttempts)
                {
                    buildedSingleQuiz.Quiz_Attempts.Add(new SingleQuizAttemptDto
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
        /// Create a single Quiz for specific User
        /// </summary>
        /// <param name="userId">The Id from User who will create a single quiz</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateSingleQuiz")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateSingleQuiz(string userId)
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

            return CreatedAtAction(nameof(GetSingleQuiz), new { singleQuizId = singleQuiz.Id });
        }
    }
}
