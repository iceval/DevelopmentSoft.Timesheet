using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Domain.Models
{
    [Serializable]
    public class ChiefEmployee : Employee
    {
        public ChiefEmployee(string lastname, decimal salary, decimal bonus) : base(lastname, salary)
        {
            Bonus = bonus;
        }

        public decimal Bonus { get; set; }

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
                    decimal bonusPerDay = MAX_WORKING_HOURS_PER_DAY / MAX_WORKING_HOURS_PER_MONTH * Bonus;
                    bill += MAX_WORKING_HOURS_PER_DAY / MAX_WORKING_HOURS_PER_MONTH * Salary + bonusPerDay;
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
            return $"{LastName}{delimeter}{Salary}{delimeter}Менеджер{delimeter}\n";
        }
    }
}