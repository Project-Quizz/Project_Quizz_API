﻿namespace Project_Quizz_API.Models.DTOs
{
    public class QuizQuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string UserId { get; set; }
        public List<QuizAnswersDto> Answers { get; set; }
    }

    public class QuizAnswersDto
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}
