using Moq;

using NUnit.Framework;

using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class VideoServiceTests
    {
        Mock<IFileReader> _fileReader;
        VideoService _videoService;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoService = new VideoService(_fileReader.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnsErrorMessage()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}
