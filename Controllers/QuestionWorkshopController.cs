using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_Quizz_API.Data;
using Project_Quizz_API.Models;
using Project_Quizz_API.Models.DTOs;
using Project_Quizz_API.Validations;

namespace Project_Quizz_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionWorkshopController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public QuestionWorkshopController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetQuestion")]
        public IActionResult GetQuestion(int id)
        {
            Quiz_Question questionFromDb = _context.Quiz_Questions.SingleOrDefault(x => x.Id == id);

            var validationErrors = GenericValidators.CheckNullOrDefault(questionFromDb, "id");
            if (validationErrors.Any())
            {
                return NotFound(validationErrors);
            }

            var question = new QuizQuestionDto
            {
                Id = questionFromDb.Id,
                QuestionText = questionFromDb.QuestionText,
                UserId = questionFromDb.UserId,
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

        [HttpPost]
        [Route("CreateQuestion")]
        public IActionResult CreateQuestion(CreateQuizQuestionDto questionDto)
        {
            var validationErrors = QuestionWorkshopControllerValidator.ValidateQuestion(questionDto);
            if (validationErrors.Any())
            {
                return BadRequest(validationErrors);
            }

            var question = new Quiz_Question
            {
                QuestionText = questionDto.QuestionText,
                UserId = questionDto.UserId,
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

        [HttpPut]
        [Route("UpdateQuestion")]
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

            questionFromDb.QuestionText = questionDto.QuestionText;
            questionFromDb.UserId = questionDto.UserId;

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
                    return NotFound();
                }
            }

            _context.SaveChanges();
            return Ok("Update Quiz Question with Answers successfully");
        }

        [HttpDelete]
        public IActionResult DeleteQuestion(int questionId)
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
    }
}
