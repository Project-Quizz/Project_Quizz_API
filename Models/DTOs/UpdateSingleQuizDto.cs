namespace Project_Quizz_API.Models.DTOs
{
    public class UpdateSingleQuizDto
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public bool QuizCompleted { get; set; }

        public List<UpdateSingleQuizAttemptDto> Quiz_Attempts { get; set; }
    }

    public class UpdateSingleQuizAttemptDto
    {
        public int Id { get; set; }

        public int? GivenAnswerId { get; set; }

        public DateTime? AnswerDate { get; set; }
    }
}
