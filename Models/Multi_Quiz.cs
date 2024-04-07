using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Quizz_API.Models
{
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
