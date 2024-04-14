namespace Project_Quizz_API.Models.DTOs
{
    /// <summary>
    /// Quiz categorie dto. It contains the id of the category and the name of the category.
    /// </summary>
    public class QuizCategorieDto
    {
        public int CategorieId { get; set; }
        public string Name { get; set; }
    }
}
