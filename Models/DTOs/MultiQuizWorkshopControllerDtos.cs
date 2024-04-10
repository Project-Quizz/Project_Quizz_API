namespace Project_Quizz_API.Models.DTOs
{
	public class GetQuestionFromMultiQuizSession
	{
		public int QuizId { get; set; }
		public string UserId { get; set; }
	}

	public class InitMultiplayerSessionDto
	{
		public string UserOne { get; set; }
		public string UserTwo { get; set; }
		public int CategorieId { get; set; }
	}

	public class QuizQuestionForMultiQuizDto
	{
		public int QuestionId { get; set; }
		public int QuizId { get; set; }
		public int? QuestionCount { get; set; }
		public string QuestionText { get; set; }
		public List<QuizAnswersDto> Answers { get; set; }
	}

	public class UpdateMultiQuizSessionDto
	{
		public int QuizId { get; set; }
		public int QuestionId { get; set; }
		public List<MultiQuizGivenAnswerIdsDto> GivenAnswerIds { get; set; }
		public string UserId { get; set; }
	}

    public class MultiQuizGivenAnswerIdsDto
    {
		public int QuizQuestionAnswerId { get; set; }
	}

    public class ResultMultiQuizDto
	{
		public int Id { get; set; }

		public int Score { get; set; }

		public bool QuizCompleted { get; set; }
		public bool MultiQuizComplete { get; set; }

		public int? QuestionCount { get; set; }

		public OpponentDto Opponent { get; set; } 
	}

	public class OpponentDto
	{
		public string UserId { get; set; }
		public int Score { get; set; }
		public bool QuizComplete { get; set; }
	}

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
