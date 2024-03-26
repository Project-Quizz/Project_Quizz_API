using System.ComponentModel.DataAnnotations;

namespace Project_Quizz_API.Models
{
    public class Quiz_Question
    {
        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public string UserId {  get; set; } 

        public virtual ICollection<Quiz_Answer> Answers { get; set; }
    }
}
