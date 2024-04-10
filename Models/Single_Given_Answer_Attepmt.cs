using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Quizz_API.Models
{
	public class Single_Given_Answer_Attepmt
	{
		public int Id { get; set; }


		[ForeignKey("Single_Quiz_Attempt")]
		public int SingleQuizAttemptId { get; set; }
		public virtual Single_Quiz_Attempt Single_Quiz_Attempt { get; set; }


		[ForeignKey("Quiz_Question_Answer")]
		public int QuizQuestionAnswerId { get; set; }
		public virtual Quiz_Question_Answer Quiz_Question_Answer { get; set; }
	}
}
