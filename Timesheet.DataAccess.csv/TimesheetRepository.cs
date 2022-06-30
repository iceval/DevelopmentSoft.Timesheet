using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.DataAccess.CSV
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private const char DELIMETER = ';';
        private const string PATH = "..\\Timesheet.DataAccess.CSV\\Data\\timesheet.csv";

        public void Add(TimeLog timelog)
        {

            var dateRow = $"{timelog.Comment}{DELIMETER}" +
                $"{timelog.Date}{DELIMETER}" +
                $"{timelog.LastName}{DELIMETER}" +
                $"{timelog.WorkingHours}\n";

            File.AppendAllText(PATH, dateRow);
        }

        public TimeLog[] GetTimeLogs(string lastName)
        {
            
            var data = File.ReadAllText("timesheet.csv");
            var timeLogs = new List<TimeLog>();

            foreach (var dataRow in data.Split("\n"))
            {
                var timeLog = new TimeLog();
                
                var dataMembers = dataRow.Split(DELIMETER);
                timeLog.Comment = dataMembers[0];
                timeLog.Date = DateTime.TryParse(dataMembers[1], out var date) ? date : new DateTime();
                timeLog.LastName = dataMembers[2];
                timeLog.WorkingHours = int.TryParse(dataMembers[3], out var workingHours) ? workingHours : 0;
            }

            return timeLogs.ToArray();
        }
    }
}
