using System;
using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class HousekeeperServiceTests
    {
        HousekeeperService _service;
        Mock<IStatementGenerator> _statementGenerator;
        Mock<IEmailSender> _emailSender;
        Mock<IXtraMessageBox> _messageBox;
        DateTime _statementDate = new DateTime(2017, 1, 1);
        Housekeeper _housekeeper;
        string _statementFileName;

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _statementGenerator = new Mock<IStatementGenerator>();
            _statementFileName = "fileName";
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(
                unitOfWork.Object,
                _statementGenerator.Object,
                _emailSender.Object,
                _messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GeneratesStatement()
        {
            _service.SendStatementEmails(_statementDate);

            VerifyStatementGenerated();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_HousekeepersEmailIsNullOrEmptyOrWhiteSpace_ShouldNotGenerateStatement(string email)
        {
            _housekeeper.Email = email;

            _service.SendStatementEmails(_statementDate);

            VerifyStatementNotGenerated();
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _service.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SendStatementEmails_StatementFileNameIsNullOrEmptyOrWhiteSpace_ShouldNotEmailTheStatement(string statementFileName)
        {
            _statementFileName = statementFileName;

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            )).Throws<Exception>();

            _service.SendStatementEmails(_statementDate);

            VerifyMessageBoxDisplayed();
        }

        void VerifyStatementGenerated()
        {
            _statementGenerator.Verify(sg =>
                    sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        void VerifyStatementNotGenerated()
        {
            _statementGenerator.Verify(sg =>
                    sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate),
                Times.Never);
        }

        void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                    _housekeeper.Email,
                    _housekeeper.StatementEmailBody,
                    _statementFileName,
                    It.IsAny<string>()));
        }

        void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }

        void VerifyMessageBoxDisplayed()
        {
            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
    }
}
