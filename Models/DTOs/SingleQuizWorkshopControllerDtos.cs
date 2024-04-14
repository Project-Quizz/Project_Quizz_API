namespace Project_Quizz_API.Models.DTOs
{
    /// <summary>
    /// Single quiz attempt dto. It contains the id of the single quiz attempt, 
    /// the id of the asked question, the id of the given answer and the date of the answer.
    /// </summary>
    public class SingleQuizAttemptDto
    {
        public int Id { get; set; }

        public int AskedQuestionId { get; set; }

        public int? GivenAnswerId { get; set; }

        public DateTime? AnswerDate { get; set; }
    }

    /// <summary>
    /// Single quiz attempt dto. It contains the id of the single quiz attempt, 
    /// the id of the asked question, the id of the given answer and the date of the answer.
    /// </summary>
    public class ResultSingleQuizDto
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public bool QuizCompleted { get; set; }

        public int? QuestionCount { get; set; }
    }

    /// <summary>
    /// Single quiz attempt dto. It contains the id of the single quiz attempt, 
    /// the id of the asked question, the id of the given answer and the date of the answer.
    /// </summary>
    public class GetSingleQuizzesFromUserDto
    {
        public int QuizId { get; set; }
        public DateTime QuizCreated { get; set; }
        public bool UserCompletedQuiz { get; set; }
        public int Score { get; set; }
        public QuizCategorieDto Categorie { get; set; }
    }

    /// <summary>
    /// Single quiz attempt dto. It contains the id of the single quiz attempt,
    /// the id of the asked question, the id of the given answer and the date of the answer.
    /// </summary>
    public class UpdateSingleQuizSessionDto
    {
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
		public List<SingleQuizGivenAnswerIdsDto> GivenAnswerIds { get; set; }
		public string UserId { get; set; }
    }

    /// <summary>
    /// Single quiz attempt dto. It contains the id of the single quiz attempt, 
    /// the id of the asked question, the id of the given answer and the date of the answer.
    /// </summary>
	public class SingleQuizGivenAnswerIdsDto
	{
		public int QuizQuestionAnswerId { get; set; }
	}
}
