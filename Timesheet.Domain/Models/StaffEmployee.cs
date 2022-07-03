using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Models
{
    public class StaffEmployee : Employee
    {
        public StaffEmployee(string lastname, decimal salary) : base(lastname, salary, "Staff")
        {
        }

        public override decimal CalculateBill(TimeLog[] timeLogs)
        {
            var totalHours = timeLogs.Sum(x => x.WorkingHours);
            decimal bill = 0;
            var WorkingHoursGroupByDay = timeLogs
                                            .GroupBy(x => x.Date.ToShortDateString());

            foreach (var workingLogsPerDay in WorkingHoursGroupByDay)
            {
                int dayHours = workingLogsPerDay.Sum(x => x.WorkingHours);

                if (dayHours > MAX_WORKING_HOURS_PER_DAY)
                {
                    var overtime = dayHours - MAX_WORKING_HOURS_PER_DAY;

                    bill += MAX_WORKING_HOURS_PER_DAY / MAX_WORKING_HOURS_PER_MONTH * Salary;
                    bill += overtime / MAX_WORKING_HOURS_PER_MONTH * Salary * 2;
                }
                else
                {
                    bill += dayHours / MAX_WORKING_HOURS_PER_MONTH * Salary;
                }
            }

            return bill;
        }

        public override string GetPersonalData(char delimeter)
        {
            return $"{LastName}{delimeter}{Salary}{delimeter}Штатный сотрудник{delimeter}\n";
        }

        public override bool CheckInputLog(TimeLog timeLog)
        {
            bool isValid = base.CheckInputLog(timeLog);
            isValid = timeLog.LastName == this.LastName && isValid;
            return isValid;
        }
    }
}