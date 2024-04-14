using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Quizz_API.Models
{
    /// <summary>
    /// Multi quiz. It contains the id of the quiz, the date of the creation, the number of questions, 
    /// the id of the quiz category and the players and attempts of the quiz.
    /// </summary>
    public class Multi_Quiz
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public bool QuizCompleted { get; set; } = false;

        public int QuestionCount { get; set; }

		[ForeignKey("Quiz_Categorie")]
        public int QuizCategorieId { get; set; }



		public virtual ICollection<Multi_Quiz_Player> Multi_Quiz_Players { get; set; }
        public virtual ICollection<Multi_Quiz_Attempt> Multi_Quiz_Attempts { get; set; }
        public virtual Quiz_Categorie Quiz_Categorie { get; set; }
    }
}
