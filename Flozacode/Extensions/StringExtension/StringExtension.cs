using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Flozacode.Extensions.StringExtension
{
    public static class StringExtension
    {
        private const string EncryptKey = "2BAE2C2C-4AC7-4C53-A98B-0C993E7BDD85";

        /// <summary>
        /// return true or throw new Exception
        /// </summary>
        /// <param name="text"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static void ThrowIfNull(this string text, Exception exception)
        {
            if (text.IsNullOrEmpty())
            {
                throw exception;
            }
        }

        /// <summary>
        /// Get array of byte from string with UTF8 Format
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] GetBytes(this string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        /// <summary>
        /// Return string from bytes[]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetStringToBytes(this string value)
        {
            int NumberChars = value.Length;
            byte[] bytes = new byte[NumberChars / 2];

            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            }

            return bytes;
        }

        /// <summary>
        /// Return boolean to indicate the string is null or just contains ''
        /// </summary>
        /// <param name="text"></param>
        /// <returns>bool</returns>
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        /// <summary>
        /// Convert text into MD5 format
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string ToMD5(this string text)
        {
            byte[] bytes = text.GetBytes();
            byte[] data = MD5.Create().ComputeHash(bytes);

            StringBuilder stringBuilder = new();

            for (int i = 0; i < data.Length; i += 1)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Convert string to title case alias Pascal Case
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToTitleCase(this string text)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(text);
        }

        /// <summary>
        /// Convert string to base64 format
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToBase64(this string text)
        {
            var bytes = text.GetBytes();
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Convert base64 to plain string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FromBase64(this string text)
        {
            var decoded = Convert.FromBase64String(text);
            return Encoding.UTF8.GetString(decoded);
        }

        /// <summary>
        /// Return a string to snake case format (snake_case)
        /// </summary>
        /// <param name="text"></param>
        /// <returns>string</returns>
        public static string ToSnakeCase(this string text)
        {
            return text.SeparateString("_");
        }

        /// <summary>
        /// Return a string to kebab case format (kebab-case)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToKebabCase(this string text)
        {
            return text.SeparateString("-");
        }

        /// <summary>
        /// Return a string to pascal case format (PascalCase)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string text)
        {
            return text.ToTitleCase().ToAlpha();
        }

        /// <summary>
        /// Seperate string with any separator
        /// </summary>
        /// <param name="text"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string SeparateString(this string text, string separator)
        {
            Regex pattern = new(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
            return string.Join(separator, pattern.Matches(text).Cast<Match>().Select(m => m.Value)).ToLower();
        }

        /// <summary>
        /// Remove all special characters and return alpha numeric only
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToAlphaNumeric(this string text)
        {
            Regex pattern = new("[^a-zA-Z0-9]");
            return pattern.Replace(text, string.Empty);
        }

        /// <summary>
        /// Check the string is just for alphabet or number
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(this string text)
        {
            string pattern = "^[a-zA-Z0-9]";
            return Regex.IsMatch(text, pattern);
        }

        /// <summary>
        /// Remove all special characters and return alphabet only
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToAlpha(this string text)
        {
            Regex pattern = new("[^a-zA-Z]");
            return pattern.Replace(text, string.Empty);
        }

        /// <summary>
        /// Check the string is just for alphabet
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsAlpha(this string text)
        {
            string pattern = "^[a-zA-Z]";
            return Regex.IsMatch(text, pattern);
        }

        /// <summary>
        /// Remove all special characters and return number only
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToNumeric(this string text)
        {
            Regex pattern = new("[^0-9]");
            return pattern.Replace(text, string.Empty);
        }

        /// <summary>
        /// Check the string is just for numeric
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string text)
        {
            string pattern = "^[0-9]";
            return Regex.IsMatch(text, pattern);
        }

        /// <summary>
        /// Encrypt string using Aes and custom key
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(this string text, string key, EncryptType encryptType = EncryptType.Base64)
        {
            text.ThrowIfNull(new ArgumentNullException(nameof(text)));
            key.ThrowIfNull(new ArgumentNullException(nameof(key)));

            byte[] buffer = Encoding.ASCII.GetBytes(key);
            Rfc2898DeriveBytes guidKey = new(EncryptKey, buffer);

            using Aes aes = Aes.Create();

            aes.Key = guidKey.GetBytes(aes.KeySize / 8);
            aes.IV = guidKey.GetBytes(aes.BlockSize / 8);

            using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using MemoryStream memoryStream = new();
            using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
            using (StreamWriter streamWriter = new(cryptoStream))
            {
                streamWriter.Write(text);
            }

            byte[] iv = aes.IV;
            byte[] encrypted = memoryStream.ToArray();
            byte[] result = new byte[iv.Length + encrypted.Length];

            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);

            if (encryptType == EncryptType.Base64)
            {
                return Convert.ToBase64String(result);
            }

            return BitConverter.ToString(result).Replace("-", string.Empty);
        }


        /// <summary>
        /// Encrypt string using Aes and custom key
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string Encrypt(this string text, string key, string guid, EncryptType encryptType = EncryptType.Base64)
        {
            text.ThrowIfNull(new ArgumentNullException(nameof(text)));
            key.ThrowIfNull(new ArgumentNullException(nameof(key)));

            byte[] buffer = Encoding.ASCII.GetBytes(key);
            Rfc2898DeriveBytes guidKey = new(guid, buffer);

            using Aes aes = Aes.Create();

            aes.Key = guidKey.GetBytes(aes.KeySize / 8);
            aes.IV = guidKey.GetBytes(aes.BlockSize / 8);

            using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using MemoryStream memoryStream = new();
            using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
            using (StreamWriter streamWriter = new(cryptoStream))
            {
                streamWriter.Write(text);
            }

            byte[] iv = aes.IV;
            byte[] encrypted = memoryStream.ToArray();
            byte[] result = new byte[iv.Length + encrypted.Length];

            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);

            if (encryptType == EncryptType.Base64)
            {
                return Convert.ToBase64String(result);
            }

            return BitConverter.ToString(result).Replace("-", string.Empty);
        }

        /// <summary>
        /// Decrypt encrypted string with Aes and custom key
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(this string text, string key, EncryptType encryptType = EncryptType.Base64)
        {
            text.ThrowIfNull(new ArgumentNullException(nameof(text)));
            key.ThrowIfNull(new ArgumentNullException(nameof(key)));

            byte[] fullCipher = encryptType == EncryptType.Base64 ?
                Convert.FromBase64String(text):
                text.GetStringToBytes();
            
            byte[] iv = new byte[16];
            byte[] cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);

            byte[] buffer = Encoding.ASCII.GetBytes(key);
            Rfc2898DeriveBytes guidKey = new(EncryptKey, buffer);

            using Aes aes = Aes.Create();

            aes.Key = guidKey.GetBytes(aes.KeySize / 8);
            aes.IV = guidKey.GetBytes(aes.BlockSize / 8);

            using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            string result;
            using (MemoryStream memoryStream = new(cipher))
            {
                using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
                using StreamReader stream = new(cryptoStream);
                result = stream.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// Decrypt encrypted string with Aes and custom key
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static string Decrypt(this string text, string key, string guid, EncryptType encryptType = EncryptType.Base64)
        {
            text.ThrowIfNull(new ArgumentNullException(nameof(text)));
            key.ThrowIfNull(new ArgumentNullException(nameof(key)));

            byte[] fullCipher = encryptType == EncryptType.Base64 ?
                Convert.FromBase64String(text) :
                text.GetStringToBytes();

            byte[] iv = new byte[16];
            byte[] cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);

            byte[] buffer = Encoding.ASCII.GetBytes(key);
            Rfc2898DeriveBytes guidKey = new(guid, buffer);

            using Aes aes = Aes.Create();

            aes.Key = guidKey.GetBytes(aes.KeySize / 8);
            aes.IV = guidKey.GetBytes(aes.BlockSize / 8);

            using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            string result;
            using (MemoryStream memoryStream = new(cipher))
            {
                using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
                using StreamReader stream = new(cryptoStream);
                result = stream.ReadToEnd();
            }

            return result;
        }
    }
}