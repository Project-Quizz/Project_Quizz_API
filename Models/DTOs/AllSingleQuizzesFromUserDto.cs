namespace Project_Quizz_API.Models.DTOs
{
    public class AllSingleQuizzesFromUserDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int Score { get; set; }

        public DateTime CreateDate { get; set; }

        public bool QuizCompleted { get; set; }

        public int QuestionCount { get; set; }

        public List<AllSingleQuizzesAttemptDto> Quiz_Attempts { get; set; }
    }

    public class AllSingleQuizzesAttemptDto
    {
        public int Id { get; set; }

        public int AskedQuestionId { get; set; }

        public int? GivenAnswerId { get; set; }

        public DateTime? AnswerDate { get; set; }
    }
}
