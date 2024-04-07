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
}
