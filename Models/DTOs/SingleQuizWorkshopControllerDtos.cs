namespace Project_Quizz_API.Models.DTOs
{
    public class SingleQuizAttemptDto
    {
        public int Id { get; set; }

        public int AskedQuestionId { get; set; }

        public int? GivenAnswerId { get; set; }

        public DateTime? AnswerDate { get; set; }
    }

    public class ResultSingleQuizDto
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public bool QuizCompleted { get; set; }

        public int? QuestionCount { get; set; }
    }

    public class GetSingleQuizzesFromUserDto
    {
        public int QuizId { get; set; }
        public DateTime QuizCreated { get; set; }
        public bool UserCompletedQuiz { get; set; }
        public int Score { get; set; }
        public QuizCategorieDto Categorie { get; set; }
    }

    public class UpdateSingleQuizSessionDto
    {
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerFromUserId { get; set; }
        public string UserId { get; set; }
    }
}
