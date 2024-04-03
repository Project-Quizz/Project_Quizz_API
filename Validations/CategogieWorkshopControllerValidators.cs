using Project_Quizz_API.Data;

namespace Project_Quizz_API.Validations
{
    public class CategogieWorkshopControllerValidators
    {
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
