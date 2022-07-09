using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet.Api.Models
{
    public class GetEmployeeReportResponse
    {
        public string LastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public TimeLogDto[] TimeLogs { get; set; }

        public int TotalHours { get; set; }
        public decimal Bill { get; set; }
    }
}