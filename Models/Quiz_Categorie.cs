namespace Project_Quizz_API.Models
{
    /// <summary>
    /// Quiz category. It contains the id of the category, the name of the category and the date of the creation.
    /// </summary>
    public class Quiz_Categorie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Quiz_Question> Questions { get; set; }

        public virtual ICollection<Single_Quiz> SingleQuizzes { get; set; }
    }
}
