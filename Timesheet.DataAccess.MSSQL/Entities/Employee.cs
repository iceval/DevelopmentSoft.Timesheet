using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.DataAccess.MSSQL.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string Position { get; set; }

        // навигационные свойства
        public ICollection<TimeLog> timeLogs { get; set; }
    }
}