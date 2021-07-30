using System.Net;

using Moq;

using NUnit.Framework;

using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class InstallerHelperTests
    {
        Mock<IFileDownloader> _fileDownloader;
        InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnsFalse()
        {
            _fileDownloader.Setup(fd =>
                fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result == false);
        }

        [Test]
        public void DownloadInstaller_DownloadCompletes_ReturnsTrue()
        {
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result == true);
        }
    }
}
