using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.BussinessLogic.Services;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Tests
{
    public class IssuesServiceTests
    {
        [Test]
        public void Get_ShouldReturnIssues()
        {
            // arrange
            var lastname = Guid.NewGuid().ToString();
            var managerLogin = Guid.NewGuid().ToString();
            var project = Guid.NewGuid().ToString();

            var employeeRepository = new Mock<IEmployeeRepository>();

            var expectedEmployee = new StaffEmployee(lastname, 20000);

            //employeeRepository
            //    .Setup(x => x.Get(lastname))
            //    .Returns(expectedEmployee)
            //    .Verifiable();

            var issuesClientMock = new Mock<IIssuesClient>();
            var expectedIssue = new Issues
            {
                Id = 124,
                Name = "sdgds",
                SourceId = 144
            };

            issuesClientMock
                .Setup(x => x.Get(managerLogin, project))
                .ReturnsAsync(new[] { expectedIssue })
                .Verifiable();

            var service = new IssuesService(employeeRepository.Object, issuesClientMock.Object);

            // act
            var issue = service.Get(managerLogin, project);

            // assert
            issuesClientMock.VerifyAll();
            employeeRepository.VerifyAll();
            Assert.IsNotNull(issue);
            Assert.IsNotEmpty(issue);
        }
    }
}