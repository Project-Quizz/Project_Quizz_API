namespace Project_Quizz_API.Models.DTOs
{
    public class EditSingleQuizDto
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public bool QuizCompleted { get; set; }

        public List<SingleQuizAttemptDto> Quiz_Attempts { get; set; }
    }

    public class EditSingleQuizAttemptDto
    {
        public int Id { get; set; }

        public int? GivenAnswerId { get; set; }

        public DateTime? AnswerDate { get; set; }
    }
}
