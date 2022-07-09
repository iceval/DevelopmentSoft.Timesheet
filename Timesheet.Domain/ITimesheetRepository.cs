using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Models;

namespace Timesheet.Domain
{
    public interface ITimesheetRepository
    {
        TimeLog[] GetTimeLogs(string lastName);
        void Add(TimeLog timeLog);
    }
}