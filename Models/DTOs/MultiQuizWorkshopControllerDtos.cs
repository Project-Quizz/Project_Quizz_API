namespace Project_Quizz_API.Models.DTOs
{
	/// <summary>
	/// Get question from multi quiz session. It contains the quiz id and the user id.
	/// </summary>
	public class GetQuestionFromMultiQuizSession
	{
		public int QuizId { get; set; }
		public string UserId { get; set; }
	}

	/// <summary>
	/// Init multiplayer session. It contains the user one, user two and the category id.
	/// </summary>
	public class InitMultiplayerSessionDto
	{
		public string UserOne { get; set; }
		public string UserTwo { get; set; }
		public int CategorieId { get; set; }
	}

	/// <summary>
	/// Multi quiz session dto. It contains the id of the session, the user one, 
	/// the user two, the category id and the date of the creation.
	/// </summary>
	public class QuizQuestionForMultiQuizDto
	{
		public int QuestionId { get; set; }
		public int QuizId { get; set; }
		public int? QuestionCount { get; set; }
		public string QuestionText { get; set; }
		public List<QuizAnswersDto> Answers { get; set; }
	}

	/// <summary>
	/// Quiz answers dto. It contains the id of the answer and the text of the answer.
	/// </summary>
	public class UpdateMultiQuizSessionDto
	{
		public int QuizId { get; set; }
		public int QuestionId { get; set; }
		public List<MultiQuizGivenAnswerIdsDto> GivenAnswerIds { get; set; }
		public string UserId { get; set; }
	}

	/// <summary>
	/// Multi quiz given answer ids dto. It contains the id of the answer.
	/// </summary>
    public class MultiQuizGivenAnswerIdsDto
    {
		public int QuizQuestionAnswerId { get; set; }
	}

	/// <summary>
	/// Multi quiz session dto. It contains the id of the session, the user one,
	/// </summary>
    public class ResultMultiQuizDto
	{
		public int Id { get; set; }

		public int Score { get; set; }

		public bool QuizCompleted { get; set; }
		public bool MultiQuizComplete { get; set; }

		public int? QuestionCount { get; set; }

		public OpponentDto Opponent { get; set; } 
	}

	/// <summary>
	/// Opponent dto. It contains the user id, the score and if the quiz is complete.
	/// </summary>
	public class OpponentDto
	{
		public string UserId { get; set; }
		public int Score { get; set; }
		public bool QuizComplete { get; set; }
	}

	/// <summary>
	/// Get multi quizzes from user dto. It contains the id of the multi quiz, the date of the quiz,
	/// </summary>
	public class GetMultiQuizzesFromUserDto
	{
		public int MultiQuizId { get; set; }
		public DateTime QuizCreated { get; set; }
		public bool UserCompletedQuiz { get; set; }
		public int Score { get; set; }
		public string OpponentUser { get; set; }
		public QuizCategorieDto Categorie { get; set; }
	}
}
