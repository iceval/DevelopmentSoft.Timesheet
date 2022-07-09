using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.BussinessLogic.Services;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Tests
{
    partial class ReportServiceTests
    {
        [Test]
        public void GetEmployeeReport_ShouldReturnReport()
        {
            // arrange
            var timesheetRepositotyMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var expectedLastName = "Иванов";
            var expectedTotal = 8750m; // (8+8+4) / 160 * 70000
            var expectedTotalHours = 20;

            timesheetRepositotyMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new[] {
                    new TimeLog {
                        LastName = expectedLastName,
                        Date = DateTime.Now.AddDays(-2),
                        WorkingHours = 8,
                        Comment = Guid.NewGuid().ToString()
                    },
                    new TimeLog {
                        LastName = expectedLastName,
                        Date = DateTime.Now.AddDays(-1),
                        WorkingHours = 8,
                        Comment = Guid.NewGuid().ToString()
                    },
                    new TimeLog {
                        LastName = expectedLastName,
                        Date = DateTime.Now,
                        WorkingHours = 4,
                        Comment = Guid.NewGuid().ToString()
                    }
                })
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.Get(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee(expectedLastName, 70000))
                .Verifiable();

            var service = new ReportService(timesheetRepositotyMock.Object, employeeRepositoryMock.Object);

            // act
            var result = service.GetEmployeeReport(expectedLastName);

            // assert
            timesheetRepositotyMock.VerifyAll();
            employeeRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }

        [Test]
        public void GetEmployeeReport_ShouldReturnReportPerSeveralMonth()
        {
            // arrange
            var timesheetRepositotyMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var expectedLastName = "Иванов";
            // bill per hour = 60000 / 160 * 375
            // 35 * 8 * 375 + 1 * 375 * 2
            var expectedTotal = 105750m;
            var expectedTotalHours = 281;

            employeeRepositoryMock
                .Setup(x => x.Get(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee(expectedLastName, 60000))
                .Verifiable();

            timesheetRepositotyMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => {
                    TimeLog[] timelogs = new TimeLog[35];
                    DateTime dateTime = new DateTime(2020, 11, 1);
                    timelogs[0] = new TimeLog
                    {
                        LastName = expectedLastName,
                        Date = dateTime,
                        WorkingHours = 9,
                        Comment = Guid.NewGuid().ToString()
                    };
                    for (int i = 1; i < timelogs.Length; i++)
                    {
                        dateTime = dateTime.AddDays(1);
                        timelogs[i] = new TimeLog
                        {
                            LastName = expectedLastName,
                            Date = dateTime,
                            WorkingHours = 8,
                            Comment = Guid.NewGuid().ToString()
                        };
                    }
                    return timelogs;
                })
                .Verifiable();

            var service = new ReportService(timesheetRepositotyMock.Object, employeeRepositoryMock.Object);

            // act
            var result = service.GetEmployeeReport(expectedLastName);

            // assert
            timesheetRepositotyMock.VerifyAll();
            employeeRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }

        [Test]
        public void GetEmployeeReport_WithoutTimeLogs_ShouldReturnReport()
        {
            // arrange
            var timesheetRepositotyMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            var expectedLastName = "Иванов";
            var expectedTotal = 0m;
            var expectedTotalHours = 0;

            timesheetRepositotyMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new TimeLog[0])
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.Get(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee(expectedLastName, 70000))
                .Verifiable();

            var service = new ReportService(timesheetRepositotyMock.Object, employeeRepositoryMock.Object);

            // act
            var result = service.GetEmployeeReport(expectedLastName);

            // assert
            timesheetRepositotyMock.VerifyAll();
            employeeRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }

        [Test]
        public void GetEmployeeReport_Staff_TimeLogWithOvertimeForOneDay_ShouldReturnReportPerOneMonth()
        {
            // arrange
            var timesheetRepositotyMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var expectedLastName = "Иванов";
            var salary = 70000m;
            var expectedTotal = 7000; // (8m / 160m * 70000m) + (4m / 160m * 70000m * 2)
            var expectedTotalHours = 12;

            timesheetRepositotyMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new[]
                {
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Comment = Guid.NewGuid().ToString(),
                        Date = DateTime.Now.AddDays(-1),
                        WorkingHours = 12
                    }
                })
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.Get(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new StaffEmployee(expectedLastName, salary))
                .Verifiable();

            var service = new ReportService(timesheetRepositotyMock.Object, employeeRepositoryMock.Object);

            // act
            var result = service.GetEmployeeReport(expectedLastName);

            // assert
            timesheetRepositotyMock.VerifyAll();
            employeeRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }

        [Test]
        public void GetEmployeeReport_Freelancer_TimeLogWithOvertimeForOneDay_ShouldReturnReportPerOneMonth()
        {
            // arrange
            var timesheetRepositotyMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var expectedLastName = "Иванов";
            var salary = 1000m;
            var expectedTotal = 12000; // 12 * 1000m
            var expectedTotalHours = 12;

            timesheetRepositotyMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new[]
                {
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Comment = Guid.NewGuid().ToString(),
                        Date = DateTime.Now.AddDays(-1),
                        WorkingHours = 12
                    }
                })
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.Get(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new FreelancerEmployee(expectedLastName, salary))
                .Verifiable();

            var service = new ReportService(timesheetRepositotyMock.Object, employeeRepositoryMock.Object);

            // act
            var result = service.GetEmployeeReport(expectedLastName);

            // assert
            timesheetRepositotyMock.VerifyAll();
            employeeRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }

        [Test]
        public void GetEmployeeReport_Manager_TimeLogWithOvertimeForOneDay_ShouldReturnReportPerOneMonth()
        {
            // arrange
            var timesheetRepositotyMock = new Mock<ITimesheetRepository>();
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var expectedLastName = "Иванов";
            var salary = 70000m;
            var bonus = 20000m;
            var expectedTotal = 4500; // 8m / 160m * 70000m + 8m / 160m * 20000 (Manager bonus = 1000 for overtime work in one day)
            var expectedTotalHours = 12;

            timesheetRepositotyMock
                .Setup(x => x.GetTimeLogs(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new[]
                {
                    new TimeLog
                    {
                        LastName = expectedLastName,
                        Comment = Guid.NewGuid().ToString(),
                        Date = DateTime.Now.AddDays(-1),
                        WorkingHours = 12
                    }
                })
                .Verifiable();

            employeeRepositoryMock
                .Setup(x => x.Get(It.Is<string>(y => y == expectedLastName)))
                .Returns(() => new ChiefEmployee(expectedLastName, salary, bonus))
                .Verifiable();

            var service = new ReportService(timesheetRepositotyMock.Object, employeeRepositoryMock.Object);

            // act
            var result = service.GetEmployeeReport(expectedLastName);

            // assert
            timesheetRepositotyMock.VerifyAll();
            employeeRepositoryMock.VerifyAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedLastName, result.LastName);

            Assert.IsNotNull(result.TimeLogs);
            Assert.IsNotEmpty(result.TimeLogs);

            Assert.AreEqual(expectedTotal, result.Bill);
            Assert.AreEqual(expectedTotalHours, result.TotalHours);
        }

    }
}