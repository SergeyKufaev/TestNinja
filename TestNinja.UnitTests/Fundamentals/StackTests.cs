using NUnit.Framework;

using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    class StackTests
    {
        Stack<string> _stack;

        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<string>();
        }

        [Test]
        public void Push_ArgIsNull_ThrowsArgumentNullException()
        {
            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidArg_AddsTheObjectToTheStack()
        {
            _stack.Push("a");

            Assert.That(_stack.Count == 1);
        }

        [Test]
        public void Count_EmptyStack_ReturnsZero()
        {
            Assert.That(_stack.Count == 0);
        }

        [Test]
        public void Pop_EmptyStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackWithAFewObjects_ReturnsObjectOnTheTop()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            var result = _stack.Pop();

            Assert.That(result == "c");
        }

        [Test]
        public void Pop_StackWithAFewObjects_RemovesObjectOnTheTop()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            _stack.Pop();

            Assert.That(_stack.Count == 2);
        }

        [Test]
        public void Peek_EmptyStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackWithObjects_ReturnsObjectOnTopOfTheStack()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            var result = _stack.Peek();

            Assert.That(result == "c");
        }

        [Test]
        public void Peek_StackWithObjects_DoesNotRemoveTheObjectOnTopOfTheStack()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            _stack.Peek();

            Assert.That(_stack.Count == 3);
        }
    }
}
