using Project_Quizz_API.Models.DTOs;

namespace Project_Quizz_API.Validations
{
    /// <summary>
    /// Validator for the question workshop controller.
    /// </summary>
    public class QuestionWorkshopControllerValidator
    {
        /// <summary>
        /// Validates a create quiz question dto.
        /// </summary>
        /// <param name="questionDto">The question dto model</param>
        /// <returns>Return a yield return</returns>
        public static IEnumerable<string> ValidateQuestion(CreateQuizQuestionDto questionDto)
        {
            if (string.IsNullOrEmpty(questionDto.QuestionText))
            {
                yield return "Question text is required.";
            }

            if (questionDto.Answers == null || questionDto.Answers.Count != 4)
            {
                yield return "Exactly four answers are required.";
            }
        }

        /// <summary>
        /// Validates a quiz question dto.
        /// </summary>
        /// <param name="questionDto">The question dto model</param>
        /// <returns>Return a yield return</returns>
        public static IEnumerable<string> ValidateQuestion(QuizQuestionDto questionDto)
        {
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
