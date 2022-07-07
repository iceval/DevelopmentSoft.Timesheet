using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Timesheet.DataAccess.MSSQL.Repositories;

namespace Timesheet.DataAccess.MSSQL.Tests
{
    public class EmployeeRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Get()
        {
            var contextOptions = new DbContextOptionsBuilder<TimesheetContext>()
                .UseSqlServer("Server=localhost;Database=TimesheetDB;Trusted_Connection=True;")
                .Options;

            var context = new TimesheetContext(contextOptions);

            var configuration = new MapperConfiguration(x => x.AddProfile<DataAccessMappingProfile>());

            var mapper = new Mapper(configuration);

            var repository = new EmployeeRepository(context, mapper);
            var employee = repository.Get("Иванов");
        }
    }
}