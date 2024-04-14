using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Quizz_API.Models
{
	/// <summary>
	/// Multi given answer attempt. It contains the id of the multi quiz attempt and the id of the quiz question answer.
	/// </summary>
	public class Multi_Given_Answer_Attempt
	{
		public int Id { get; set; }


		[ForeignKey("Multi_Quiz_Attempt")]
		public int MultiQuizAttemptId { get; set; }
		public virtual Multi_Quiz_Attempt Multi_Quiz_Attempt { get; set; }


		[ForeignKey("Quiz_Question_Answer")]
		public int QuizQuestionAnswerId { get; set; }
		public virtual Quiz_Question_Answer Quiz_Question_Answer { get; set; }
	}
}
