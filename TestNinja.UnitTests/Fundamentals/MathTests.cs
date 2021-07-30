using NUnit.Framework;

using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    class MathTests
    {
        Math _math;

        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        public void Add_WhenCalled_ReturnsTheSumOfArguments()
        {
            var result = _math.Add(1, 2);

            Assert.That(result == 3);
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnsTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result == expectedResult);
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnsOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));
        }

        [Test]
        public void GetOddNumbers_LimitIsEqualToZero_ReturnsEmpty()
        {
            var result = _math.GetOddNumbers(0);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetOddNumbers_LimitIsLessThanZero_ReturnsEmpty()
        {
            var result = _math.GetOddNumbers(-5);

            Assert.That(result, Is.Empty);
        }
    }
}
