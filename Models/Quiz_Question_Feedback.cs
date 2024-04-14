using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Quizz_API.Models
{
	/// <summary>
	/// Quiz question feedback. It contains the id of the question, the feedback, the user id and the date of the feedback.
	/// </summary>
	public class Quiz_Question_Feedback
	{
		public int Id { get; set; }

		[ForeignKey("Quiz_Question")]
		public int QuestionId { get; set; }
		public virtual Quiz_Question Quiz_Question { get; set; }

		public string Feedback { get; set; }

		public string UserId { get; set; }

		public DateTime CreateDate { get; set; }
	}
}
