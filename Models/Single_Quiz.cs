using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.Marshalling;


namespace Project_Quizz_API.Models
{
    /// <summary>
    /// Single quiz model. It contains the id of the user, the score of the quiz, the date of the quiz, the id of the quiz category and the number of questions.
    /// </summary>
    public class Single_Quiz
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public int Score { get; set; }  

        public DateTime CreateDate { get; set; }

        public bool QuizCompleted { get; set; } = false;

        [ForeignKey("Quiz_Categorie")]
        public int QuizCategorieId { get; set; } = 0;

        public int? QuestionCount { get; set; }

        public virtual ICollection<Single_Quiz_Attempt> Quiz_Attempts { get; set; }

        public virtual Quiz_Categorie Quiz_Categorie { get; set; }
    }
}
