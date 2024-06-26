﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Project_Quizz_API.Models
{
    /// <summary>
    /// Quiz question. It contains the id of the question, the question text, the user id and the id of the quiz category.
    /// </summary>
    public class Quiz_Question
    {
        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public string UserId {  get; set; }

        [ForeignKey("Quiz_Categorie")]
        public int QuizCategorieId { get; set; }
		public virtual Quiz_Categorie Quiz_Categorie { get; set; }



		public virtual ICollection<Quiz_Question_Answer> Answers { get; set; }

		public virtual ICollection<Quiz_Question_Feedback> Question_Feedback { get; set; }

    }
}
