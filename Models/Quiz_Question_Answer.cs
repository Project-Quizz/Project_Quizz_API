using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Quizz_API.Models
{
    /// <summary>
    /// Quiz question answer. It contains the id of the question, the answer text and if the answer is correct.
    /// </summary>
    public class Quiz_Question_Answer
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Quiz_Question")]
        public int QuestionId { get; set; }

        [Required]
        public string AnswerText { get; set; }

        [Required]
        public bool IsCorrectAnswer { get; set; }

        public virtual Quiz_Question Quiz_Question { get; set; }
		public virtual ICollection<Multi_Given_Answer_Attempt> MultiQuizAttemptAnswers { get; set; }
		public virtual ICollection<Single_Given_Answer_Attepmt> SingleQuizAttemptAnswers { get; set; }
	}
}
