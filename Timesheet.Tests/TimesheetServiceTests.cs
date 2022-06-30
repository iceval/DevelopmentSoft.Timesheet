using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Models;
using Timesheet.Application.Services;
using static Timesheet.Application.Services.AuthService;
using Moq;
using Timesheet.Domain;

namespace Timesheet.Tests
{
    class TimesheetServiceTests
    {
        [Test]
        public void TrackTime_ShouldReturnTrue()
        {
            // arrange
            var expectedLastName = "TestUser";

            UserSession.Sessions.Add(expectedLastName);

            var timelog = new TimeLog
            {
                Date = new DateTime(),
                WorkingHours = 1,
                LastName = expectedLastName,
                Comment = Guid.NewGuid().ToString()
            };
            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            timesheetRepositoryMock
                .Setup(x => x.Add(timelog))
                .Verifiable();

            var service = new TimesheetService(timesheetRepositoryMock.Object);

            // act
            var result = service.TrackTime(timelog);

            // assert
            timesheetRepositoryMock.Verify(x => x.Add(timelog), Times.Once);
            Assert.IsTrue(result);
        }

        [TestCase(25, "")]
        [TestCase(25, null)]
        [TestCase(25, "TestUser")]
        [TestCase(-1, "")]
        [TestCase(-1, null)]
        [TestCase(-1, "TestUser")]
        [TestCase(4, "")]
        [TestCase(4, null)]
        [TestCase(4, "TestUser")]
        public void TrackTime_ShouldReturnFalse(int hours, string lastName)
        {
            // arrange

            var timelog = new TimeLog
            {
                Date = new DateTime(),
                WorkingHours = hours,
                LastName = lastName,
                Comment = Guid.NewGuid().ToString()
            };

            var timesheetRepositoryMock = new Mock<ITimesheetRepository>();
            timesheetRepositoryMock
                .Setup(x => x.Add(timelog))
                .Verifiable();

            var service = new TimesheetService(timesheetRepositoryMock.Object);

            // act
            timesheetRepositoryMock.Verify(x => x.Add(timelog), Times.Never);
            var result = service.TrackTime(timelog);

            // assert
            Assert.IsFalse(result);
        }
    }
}