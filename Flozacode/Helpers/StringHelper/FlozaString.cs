using System.Security.Cryptography;

namespace Flozacode.Helpers.StringHelper
{
    public static class FlozaString
    {
        /// <summary>
        /// Return random string with specific length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GenerateRandomString(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Minimum length is 1", nameof(length));
            }

            Random random = new();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string result = new(Enumerable.Range(0, length).Select(_ => chars[random.Next(chars.Length)]).ToArray());

            return result;
        }

        /// <summary>
        /// Return random 32 bytes of string
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomString()
        {
            byte[] randomNumber = new byte[32];
            using RandomNumberGenerator range = RandomNumberGenerator.Create();
            {
                range.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// Return random string of number. Usually for OTP
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomNumberString(int length = 5)
        {
            string otp = string.Empty;
            Random random = new();
            string[] characters = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

            for (int i = 0; i < length; i++) // change length of OTP here
            {
                int selectedIndex = random.Next(0, characters.Length);
                otp += characters[selectedIndex];
            }

            return otp;
        }
    }
}
