using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Quizz_API.Models
{
    public class Multi_Quiz_Attempt
    {
        public int Id { get; set; }

        [ForeignKey("Multi_Quiz")]
        public int MultiQuizId { get; set; }

        [ForeignKey("Multi_Quiz_Player")]
        public int MultiQuizPlayerId { get; set; }

        [ForeignKey("Quiz_Question")]
        public int AskedQuestionId { get; set; }

        [ForeignKey("Quiz_Question_Answer")]
        public int? GivenAnswerId { get; set; }

        public DateTime? AnswerDate { get; set; }



        public virtual Multi_Quiz Multi_Quiz { get; set; }
        public virtual Multi_Quiz_Player Multi_Quiz_Player { get; set; }
        public virtual Quiz_Question Quiz_Question { get; set; }
        public virtual Quiz_Question_Answer Quiz_Question_Answer { get; set; }
    }
}
