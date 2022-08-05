namespace Flozacode.Extensions.NumberExtension
{
    public static class NumberExtension
    {
        /// <summary>
        /// Convert an object to int.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static int ToInt(this object parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter), "Cannot passing nullable object.");
            }

            string text = parameter.ToString() ?? "";

            bool isValid = int.TryParse(text, out int result);

            if (isValid)
            {
                return result;
            }

            throw new InvalidOperationException("Object failed to parse to Int.");
        }

        /// <summary>
        /// Convert an object to long.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static long ToLong(this object parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter), "Cannot passing nullable object.");
            }

            string text = parameter.ToString() ?? "";

            bool isValid = long.TryParse(text, out long result);

            if (isValid)
            {
                return result;
            }

            throw new InvalidOperationException("Object failed to parse to Long.");
        }

        /// <summary>
        /// Convert an object to double.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static double ToDouble(this object parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter), "Cannot passing nullable object.");
            }

            string text = parameter.ToString() ?? "";

            bool isValid = double.TryParse(text, out double result);

            if (isValid)
            {
                return result;
            }

            throw new InvalidOperationException("Object failed to parse to Double.");
        }

        /// <summary>
        /// Convert an object to float.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static float ToFloat(this object parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter), "Cannot passing nullable object.");
            }

            string text = parameter.ToString() ?? "";

            bool isValid = float.TryParse(text, out float result);

            if (isValid)
            {
                return result;
            }

            throw new InvalidOperationException("Object failed to parse to Float.");

        }
    }
}
