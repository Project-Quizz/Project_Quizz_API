using Project_Quizz_API.Data;

namespace Project_Quizz_API.Validations
{
    /// <summary>
    /// Validators for the CategorieWorkshopController.
    /// </summary>
    public class CategogieWorkshopControllerValidators
    {
        /// <summary>
        /// Checks if a category is in use.
        /// </summary>
        /// <param name="categorieId">Id of the categorie</param>
        /// <param name="_context">Db context</param>
        /// <returns></returns>
        public static bool CategorieIsInUse(int categorieId, ApplicationDbContext _context)
        {
            if (CheckInQuestions(categorieId, _context))
            {
                return true;
            }
            if (CheckInSingleQuizSessions(categorieId, _context))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a category is in use in questions.
        /// </summary>
        /// <param name="categorieId">Categorie id</param>
        /// <param name="_context">Db context</param>
        /// <returns></returns>
        private static bool CheckInQuestions(int categorieId, ApplicationDbContext _context)
        {
            var questions = _context.Quiz_Questions.Where(x => x.QuizCategorieId == categorieId).ToList();
            if (questions.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a category is in use in single quiz sessions.
        /// </summary>
        /// <param name="categorieId">Categorie id</param>
        /// <param name="_context">Db context</param>
        /// <returns></returns>
        private static bool CheckInSingleQuizSessions(int categorieId, ApplicationDbContext _context)
        {
            var singleQuizSessions = _context.Single_Quizzes.Where(x => x.QuizCategorieId == categorieId).ToList();
            if (singleQuizSessions.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
