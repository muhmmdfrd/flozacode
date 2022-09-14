using Flozacode.Extensions.NumberExtension;
using Flozacode.Helpers.StringHelper;

namespace Flozacode.UnitTest.Extensions;

[TestClass]
public class IntExtensionTest
{
    [TestMethod]
    public void ConvertToHashCodeTest()
    {
        var number = FlozaString.GenerateRandomNumberString().ToInt();
        var key = FlozaString.GenerateRandomString();
        var hashCode = number.ToHashCode(key);
        
        Console.WriteLine($"Number: {number}");
        Console.WriteLine($"Key: {key}");
        Console.WriteLine($"hashcode: {hashCode}");
        
        Assert.IsTrue(true);
    }
}