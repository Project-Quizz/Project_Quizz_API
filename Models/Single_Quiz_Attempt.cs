using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Quizz_API.Models
{
    public class Single_Quiz_Attempt
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Single_Quiz")]
        public int SingleQuizId { get; set; }

        [Required]
        [ForeignKey("Quiz_Question")]
        public int AskedQuestionId { get; set; }

        [ForeignKey("Quiz_Question_Answer")]
        public int? GivenAnswerId { get; set; }

        public DateTime? AnswerDate { get; set; }

        public virtual Single_Quiz Single_Quiz { get; set; }
        public virtual Quiz_Question Quiz_Question { get; set; }
        public virtual Quiz_Question_Answer Quiz_Question_Answer { get; set; }
    }
}
