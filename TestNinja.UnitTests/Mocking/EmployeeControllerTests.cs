using Moq;

using NUnit.Framework;

using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class EmployeeControllerTests
    {
        Mock<IEmployeeStorage> _storage;
        EmployeeController _controller;

        [SetUp]
        public void SetUp()
        {
            _storage = new Mock<IEmployeeStorage>();
            _controller = new EmployeeController(_storage.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeletesTheEmployeeFromDb()
        {
            _controller.DeleteEmployee(1);

            _storage.Verify(s => s.DeleteEmployee(1));
        }

        [Test]
        public void DeleteEmployee_WhenCalled_ReturnsRedirectResult()
        {
            var result = _controller.DeleteEmployee(1);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }
    }
}
