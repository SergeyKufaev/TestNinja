using NUnit.Framework;

using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class FizzBuzzTests
    {
        [Test]
        public void GetOutout_InputIsDivisibleBy3And5_ReturnsFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(15);

            Assert.That(result == "FizzBuzz");
        }

        [Test]
        public void GetOutout_InputIsDivisibleBy3Only_ReturnsFizz()
        {
            var result = FizzBuzz.GetOutput(3);

            Assert.That(result == "Fizz");
        }

        [Test]
        public void GetOutout_InputIsDivisibleBy5Only_ReturnsBuzz()
        {
            var result = FizzBuzz.GetOutput(5);

            Assert.That(result == "Buzz");
        }

        [Test]
        public void GetOutout_InputIsNotDivisibleBy3Or5_ReturnsTheSameNumber()
        {
            var result = FizzBuzz.GetOutput(1);

            Assert.That(result == "1");
        }
    }
}
