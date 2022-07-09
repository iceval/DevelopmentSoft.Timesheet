using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.DataAccess.MSSQL.Repositories;

namespace Timesheet.DataAccess.MSSQL.Tests
{
    class TimesheetRepositoryTests
    {
        [Test]
        public void Update()
        {
            var contextOptions = new DbContextOptionsBuilder<TimesheetContext>()
                .UseSqlServer("Server=localhost;Database=TimesheetDB;Trusted_Connection=True;")
                .Options;

            var context = new TimesheetContext(contextOptions);

            var configuration = new MapperConfiguration(x => x.AddProfile<DataAccessMappingProfile>());

            var mapper = new Mapper(configuration);

            var repository = new TimesheetRepository(context, mapper);

            // запрос списка таймлогов
            var timeLogs = repository.GetTimeLogs("Иванов");

            // запрос деталей таймлога
            var updatedTimeLog = repository.Get(8);

            updatedTimeLog.Comment = "New Comment";

            // обновление конкретного таймлога

            var timeLog = repository.Get(updatedTimeLog.Id);

            if (timeLog == null)
                throw new Exception("все пропало");

            repository.Update(updatedTimeLog);
        }
    }
}