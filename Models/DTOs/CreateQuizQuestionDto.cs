namespace Project_Quizz_API.Models.DTOs
{
    public class CreateQuizQuestionDto
    {
        public string QuestionText { get; set; }
        public string UserId { get; set; }
        public List<CreateQuizAnswersDto> Answers { get; set; }
    }

    public class CreateQuizAnswersDto
    {
        public string AnswerText { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}
