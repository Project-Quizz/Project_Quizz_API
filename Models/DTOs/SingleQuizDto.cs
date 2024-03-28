namespace Project_Quizz_API.Models.DTOs
{
    public class SingleQuizDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int Score { get; set; }

        public DateTime CreateDate { get; set; }

        public bool QuizCompleted { get; set; }

        public List<SingleQuizAttemptDto> Quiz_Attempts { get; set; }
    }

    public class SingleQuizAttemptDto
    {
        public int Id { get; set; }

        public int AskedQuestionId { get; set; }

        public int? GivenAnswerId { get; set; }

        public DateTime? AnswerDate { get; set; }
    }
}
