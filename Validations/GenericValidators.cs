namespace Project_Quizz_API.Validations
{
    /// <summary>
    /// Generic validators.
    /// </summary>
    public class GenericValidators
    {
        /// <summary>
        /// Check if the object is null or default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">Object to check</param>
        /// <param name="variableName">Variable name if necessary</param>
        /// <returns></returns>
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

        /// <summary>
        /// Check if the object exists in the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">Object to check</param>
        /// <param name="variableName">Variable name if is necessary</param>
        /// <returns></returns>
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
