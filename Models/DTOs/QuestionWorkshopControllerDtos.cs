namespace Project_Quizz_API.Models.DTOs
{
    public class QuestionFromUserDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public QuizCategorieDto Categorie { get; set; }
        public int FeedbackCount { get; set; }
    }

    public class CreateQuizQuestionDto
    {
        public string QuestionText { get; set; }
        public string UserId { get; set; }
        public CreateQuizCategorieDto Categorie { get; set; }
        public List<CreateQuizAnswersDto> Answers { get; set; }
    }

    public class CreateQuizAnswersDto
    {
        public string AnswerText { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }

    public class CreateQuizCategorieDto
    {
        public int CategorieId { get; set; }
    }

    public class QuizQuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string UserId { get; set; }
        public QuizCategorieDto Categorie { get; set; }
        public List<QuizAnswersDto> Answers { get; set; }
    }

    public class QuizAnswersDto
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }

    public class QuizQuestionForSingleQuizDto
    {
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public int? QuestionCount { get; set; }
        public string QuestionText { get; set; }
        public List<QuizAnswersDto> Answers { get; set; }
    }

    public class QuizQuestionFeedbackDto
    {
        public int QuestionId { get; set; }
        public string Feedback { get; set; }
        public string UserId { get; set; }
    }

	public class GetQuizQuestionFeedbackDto
	{
		public int QuestionId { get; set; }
		public string Feedback { get; set; }
		public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
	}
}
