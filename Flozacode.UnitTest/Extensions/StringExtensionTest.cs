using Flozacode.Extensions.StringExtension;
using Flozacode.Helpers.StringHelper;

namespace Flozacode.UnitTest.Extensions
{
    [TestClass]
    public class StringExtensionTest
    {
        [TestMethod]
        public void AnyOrThrowTest()
        {
            try
            {
                var text = "";
                text.ToAlpha();

                Assert.IsFalse(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void Bas64Test()
        {
            var text = "Test.asmx";
            var base64 = text.ToBase64();
            var decoded = base64.FromBase64();

            Console.WriteLine($"base64: {base64}");
            Console.WriteLine($"plain: {decoded}");

            Assert.AreEqual(text, decoded);
        }

        [TestMethod]
        public void TitleCaseTest()
        {
            var text = "tHe heisT";
            Console.WriteLine(text.ToTitleCase());
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ToMd5Test()
        {
            var text = "test";

            Console.WriteLine($"MD5: {text.ToMD5()}");

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ToSnakeCaseTest()
        {
            var text = "SnakeCase";
            var textSnakeCase = text.ToSnakeCase();
            Console.WriteLine(textSnakeCase);

            Assert.IsTrue(textSnakeCase.Contains('_'));
        }

        [TestMethod]
        public void ToKebabCaseTest()
        {
            var text = "kebabCase";
            var textKebabCase = text.ToKebabCase();
            Console.WriteLine(textKebabCase);

            Assert.IsTrue(textKebabCase.Contains('-'));
        }

        [TestMethod]
        public void ToPascalCaseTest()
        {
            var text = "kebab-case";
            var pascal = text.ToPascalCase();
            Console.WriteLine(pascal);

            Assert.IsTrue(!pascal.Contains(' '));
        }

        [TestMethod]
        public void ToAlphaTest()
        {
            var text = "kmzwa8awaa";
            var result = text.ToAlpha();
            Console.WriteLine(result);
            Assert.IsTrue(result.IsAlpha());
            Assert.IsFalse(result.IsNumeric());
            Assert.IsTrue(result.IsAlphaNumeric());
        }

        [TestMethod]
        public void EncryptDecryptTest()
        {
            var text = "Muhammad Farid";
            var key = FlozaString.GenerateRandomString(6);
            var encrypted = text.Encrypt(key);
            var decrypted = encrypted.Decrypt(key);

            Console.WriteLine("key: " + key);
            Console.WriteLine("Encrypted: " + encrypted);
            Console.WriteLine("Decrypted: " + decrypted);

            Assert.IsTrue(decrypted == text);
        }
    }
}
