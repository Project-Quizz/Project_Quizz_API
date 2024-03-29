using Project_Quizz_API.Models.DTOs;

namespace Project_Quizz_API.Validations
{
    public class QuestionWorkshopControllerValidator
    {
        public static IEnumerable<string> ValidateQuestion(CreateQuizQuestionDto questionDto)
        {
            if (questionDto == null)
            {
                yield return "Question data must not be null.";
            }

            if (string.IsNullOrEmpty(questionDto.QuestionText))
            {
                yield return "Question text is required.";
            }

            if (questionDto.Answers == null || questionDto.Answers.Count != 4)
            {
                yield return "Exactly four answers are required.";
            }
        }

        public static IEnumerable<string> ValidateQuestion(QuizQuestionDto questionDto)
        {
            if (questionDto == null)
            {
                yield return "Question data must not be null.";
            }

            if (string.IsNullOrEmpty(questionDto.QuestionText))
            {
                yield return "Question text is required.";
            }

            if (questionDto.Answers == null || questionDto.Answers.Count != 4)
            {
                yield return "Exactly four answers are required.";
            }
        }
    }
}
