namespace Project_Quizz_API.Models.DTOs
{
    public class QuestionFromUserDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public QuizCategorieDto Categorie { get; set; }
    }
}
