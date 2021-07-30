using NUnit.Framework;

using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_EnclosesTheStringWithStrongElement()
        {
            var formatter = new HtmlFormatter();

            var result = formatter.FormatAsBold("abc");

            // Specific
            Assert.That(result == "<strong>abc</strong>");

            // More general
            //Assert.That(result, Does.StartWith("<strong>"));
            //Assert.That(result, Does.Contain("abc"));
            //Assert.That(result, Does.EndWith("</strong>"));
        }
    }
}
