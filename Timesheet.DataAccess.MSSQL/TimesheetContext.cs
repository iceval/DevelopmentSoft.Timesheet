using Microsoft.EntityFrameworkCore;
using System;
using Timesheet.DataAccess.MSSQL.Entities;

namespace Timesheet.DataAccess.MSSQL
{
    public class TimesheetContext : DbContext
    {
        public TimesheetContext(DbContextOptions<TimesheetContext> options) : base(options)
        {

        }

        public DbSet<TimeLog> TimeLogs { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }
}