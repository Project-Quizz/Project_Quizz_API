﻿using System.Diagnostics.CodeAnalysis;

namespace Project_Quizz_API.Models.DTOs
{
    public class QuizQuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string UserId { get; set; }
        public QuizCategorieDto Categorie { get; set; }
        public List<QuizAnswersDto> Answers { get; set; }
    }

    public class QuizAnswersDto
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }

    public class QuizQuestionForSingleQuizDto
    {
		public int QuestionId { get; set; }
        public int QuizId { get; set; }
        public int? QuestionCount { get; set; }
		public string QuestionText { get; set; }
		public List<QuizAnswersDto> Answers { get; set; }
	}
}
