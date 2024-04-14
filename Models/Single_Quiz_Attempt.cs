using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Quizz_API.Models
{
    /// <summary>
    /// Attempts of an single quiz. It contains the id of the quiz, the id of the question and the date of the answer.
    /// </summary>
    public class Single_Quiz_Attempt
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Single_Quiz")]
        public int SingleQuizId { get; set; }

        [Required]
        [ForeignKey("Quiz_Question")]
        public int AskedQuestionId { get; set; }

        public DateTime? AnswerDate { get; set; }

        public virtual Single_Quiz Single_Quiz { get; set; }
        public virtual Quiz_Question Quiz_Question { get; set; }
		public virtual ICollection<Single_Given_Answer_Attepmt> SingleQuizQuestionAnswers { get; set; }
	}
}
