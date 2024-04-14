using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Quizz_API.Models
{
    /// <summary>
    /// Multi quiz player. It contains the id of the quiz, the user id, the score, the question count and if the quiz is complete.
    /// </summary>
    public class Multi_Quiz_Player
    {
        public int Id { get; set; }

        [ForeignKey("Multi_Quiz")]
        public int MultiQuizId { get; set; }

        public string UserId { get; set; }

        public int Score { get; set; }
		public int QuestionCount { get; set; } = 0;
        public bool QuizComplete { get; set; } = false;



		public virtual Multi_Quiz Multi_Quiz { get; set; }
        public virtual ICollection<Multi_Quiz_Attempt> Multi_Quiz_Attempts { get; set; }
    }
}
