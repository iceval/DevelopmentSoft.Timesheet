using Moq;
using NUnit.Framework;
using System;
using Timesheet.BussinessLogic.Exceptions;
using Timesheet.BussinessLogic.Services;
using Timesheet.Domain;
using Timesheet.Domain.Models;
using static Timesheet.BussinessLogic.Services.AuthService;

namespace Timesheet.Tests
{
    public class AuthServiceTests
    {
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private AuthService _service;

        [SetUp]
        public void Setup()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _service = new AuthService(_employeeRepositoryMock.Object);
        }

        [TestCase("??????")]
        [TestCase("??????")]
        [TestCase("???????")]
        public void Login_ShouldReturnToken(string lastName)
        {
            // arrange
            var secret = Guid.NewGuid().ToString();

            _employeeRepositoryMock.Setup(x => x.Get(It.Is<string>(y => y == lastName)))
                .Returns(() => new StaffEmployee(lastName, 70000))
                .Verifiable();

            // act
            var result = _service.Login(lastName, secret);

            // assert
            _employeeRepositoryMock.VerifyAll();

            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        public void Login_InvokeLoginTwiceForOneLastName_ShouldReturnTrue()
        {
            // arrange
            var secret = Guid.NewGuid().ToString();
            var lastName = "??????";

            _employeeRepositoryMock.Setup(x => x.Get(It.Is<string>(y => y == lastName)))
                .Returns(() => new StaffEmployee(lastName, 70000))
                .Verifiable();

            // act
            var token1 = _service.Login(lastName, secret);
            var token2 = _service.Login(lastName, secret);

            // assert
            _employeeRepositoryMock.VerifyAll();

            Assert.False(string.IsNullOrWhiteSpace(token1));
            Assert.False(string.IsNullOrWhiteSpace(token2));
            Assert.True(token1 != token2);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Login_NotValidArgument_ShouldThrowArgumentException(string lastName)
        {
            // arrange
            var secret = Guid.NewGuid().ToString();

            // act
            string result = null;
            Assert.Throws<ArgumentException>(() => _service.Login(lastName, secret));

            // assert
            _employeeRepositoryMock.Verify(x => x.Get(lastName), Times.Never);

            Assert.IsNull(result);
        }

        [TestCase("TestUser")]
        public void Login_UserDoesntExist_ShouldThrowNotFoundException(string lastName)
        {
            // arrange
            var secret = Guid.NewGuid().ToString();

            // act
            string result = null;
            Assert.Throws<NotFoundException>(() => result = _service.Login(lastName, secret));

            // assert
            _employeeRepositoryMock.Verify(x => x.Get(lastName), Times.Once);

            Assert.IsNull(result);
            //sAssert.IsTrue(UserSession.Sessions.Contains(lastName) == false);
        }
    }
}