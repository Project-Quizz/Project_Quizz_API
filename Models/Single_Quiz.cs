using System.ComponentModel.DataAnnotations;

namespace Project_Quizz_API.Models
{
    public class Single_Quiz
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public int Score { get; set; }  

        public DateTime CreateDate { get; set; }

        public bool QuizCompleted { get; set; } = false;

        public virtual ICollection<Single_Quiz_Attempt> Quiz_Attempts { get; set; }
    }
}
