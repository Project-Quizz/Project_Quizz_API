using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Quizz_API.Models
{
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
