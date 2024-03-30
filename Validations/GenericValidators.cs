namespace Project_Quizz_API.Validations
{
    public class GenericValidators
    {
        public static IEnumerable<string> CheckNullOrDefault<T>(T obj, string variableName = null)
        {
            if (variableName == null)
            {
                variableName = typeof(T).Name;
            }

            if (obj == null)
            {
                yield return $"{variableName} must not be null.";
            }
            else if (obj is string str && string.IsNullOrEmpty(str))
            {
                yield return $"{variableName} cannot be null or empty";
            }

            if (typeof(T).IsValueType && Equals(obj, default(T)))
            {
                yield return $"{variableName} cannot be default";
            }
        }

        public static IEnumerable<string> CheckIfObjectExist<T>(T obj, string variableName = null)
        {
            if (variableName == null)
            {
                variableName = typeof(T).Name;
            }

            if (obj == null)
            {
                yield return $"{variableName} does not exist in the database.";
            }
        }
    }
}
