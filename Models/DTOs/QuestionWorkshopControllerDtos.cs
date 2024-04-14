namespace Project_Quizz_API.Models.DTOs
{
    /// <summary>
    /// Question from user dto. It contains the id of the question, the text of the question,
    /// the category of the question and the feedback count.
    /// </summary>
    public class QuestionFromUserDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public QuizCategorieDto Categorie { get; set; }
        public int FeedbackCount { get; set; }
    }

    /// <summary>
    /// Quiz category dto. It contains the id of the category and the name of the category.
    /// </summary>
    public class CreateQuizQuestionDto
    {
        public string QuestionText { get; set; }
        public string UserId { get; set; }
        public CreateQuizCategorieDto Categorie { get; set; }
        public List<CreateQuizAnswersDto> Answers { get; set; }
    }

    /// <summary>
    /// Create quiz answers dto. It contains the text of the answer and if the answer is correct.
    /// </summary>
    public class CreateQuizAnswersDto
    {
        public string AnswerText { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }

    /// <summary>
    /// Quiz category dto. It contains the id of the category and the name of the category.
    /// </summary>
    public class CreateQuizCategorieDto
    {
        public int CategorieId { get; set; }
    }

    /// <summary>
    /// Quiz category dto. It contains the id of the category and the name of the category.
    /// </summary>
    public class QuizQuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string UserId { get; set; }
        public QuizCategorieDto Categorie { get; set; }
        public List<QuizAnswersDto> Answers { get; set; }
    }

    /// <summary>
    /// Quiz category dto. It contains the id of the category and the name of the category.
    /// </summary>
    public class QuizAnswersDto
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }

    /// <summary>
    /// Quiz category dto. It contains the id of the category and the name of the category.
    /// </summary>
    public class QuizQuestionForSingleQuizDto
    {
        public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public int? QuestionCount { get; set; }
        public string QuestionText { get; set; }
        public List<QuizAnswersDto> Answers { get; set; }
    }

    /// <summary>
    /// Quiz category dto. It contains the id of the category and the name of the category.
    /// </summary>
    public class QuizQuestionFeedbackDto
    {
        public int QuestionId { get; set; }
        public string Feedback { get; set; }
        public string UserId { get; set; }
    }

    /// <summary>
    /// Quiz category dto. It contains the id of the category and the name of the category.
    /// </summary>
	public class GetQuizQuestionFeedbackDto
	{
		public int QuestionId { get; set; }
		public string Feedback { get; set; }
		public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
	}
}
