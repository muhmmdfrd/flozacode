using Flozacode.Helpers.StringHelper;

namespace Flozacode.UnitTest.Helpers.StringHelper
{
    [TestClass]
    public class FlozaStringTest
    {
        [TestMethod]
        public void GenerateRandomStringTest()
        {
            var result = FlozaString.GenerateRandomString();
            Console.WriteLine(result);
            Assert.IsTrue(true);
        }
    }
}
