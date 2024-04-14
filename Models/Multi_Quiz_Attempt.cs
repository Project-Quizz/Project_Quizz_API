using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Quizz_API.Models
{
    /// <summary>
    /// Attempts of an multi quiz. It contains the id of the quiz, the id of the player, the id of the question and the date of the answer.
    /// </summary>
    public class Multi_Quiz_Attempt
    {
        public int Id { get; set; }

        [ForeignKey("Multi_Quiz")]
        public int MultiQuizId { get; set; }

        [ForeignKey("Multi_Quiz_Player")]
        public int MultiQuizPlayerId { get; set; }

        [ForeignKey("Quiz_Question")]
        public int AskedQuestionId { get; set; }

        public DateTime? AnswerDate { get; set; }



        public virtual Multi_Quiz Multi_Quiz { get; set; }
        public virtual Multi_Quiz_Player Multi_Quiz_Player { get; set; }
        public virtual Quiz_Question Quiz_Question { get; set; }
		public virtual ICollection<Multi_Given_Answer_Attempt> MultiQuizQuestionAnswers { get; set; }
	}
}
