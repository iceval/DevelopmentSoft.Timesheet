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
    class EmployeeServiceTests
    {
        [Test]
        [TestCase("Иванов", 20000)]
        [TestCase("Петров", 30000)]
        [TestCase("Сидоров", 40000)]
        public void Add_ShouldReturnTrue(string lastname, int salary)
        {
            // arrange
            var staffEmployee = new StaffEmployee(lastname, salary);
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();
            employeeRepositoryMock
                .Setup(x => x.Add(staffEmployee))
                .Verifiable();

            var service = new EmployeeService(employeeRepositoryMock.Object);

            // act
            var result = service.Add(staffEmployee);

            // assert
            employeeRepositoryMock.Verify(x => x.Add(staffEmployee), Times.Once);

            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("Иванов", 0)]
        [TestCase("Иванов", -1000)]
        [TestCase("", 40000)]
        [TestCase(null, 40000)]
        public void Add_ShouldReturnFalse(string lastname, int salary)
        {
            // arrange
            var staffEmployee = new StaffEmployee(lastname, salary);
            var employeeRepositoryMock = new Mock<IEmployeeRepository>();

            var service = new EmployeeService(employeeRepositoryMock.Object);

            // act
            var result = service.Add(staffEmployee);

            // assert
            employeeRepositoryMock.Verify(x => x.Add(It.IsAny<StaffEmployee>()), Times.Never);

            Assert.IsFalse(result);
        }
    }
}