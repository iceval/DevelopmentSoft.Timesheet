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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly char _delimeter;
        private readonly string _path;

        public EmployeeRepository(CsvSettings csvSettings)
        {
            _delimeter = csvSettings.Delimeter;
            _path = csvSettings.Path + "\\employees.csv";
        }

        public void AddEmployee(StaffEmployee staffEmployee)
        {
            var dataRow = $"{staffEmployee.LastName}{_delimeter}" +
                $"{staffEmployee.Salary}\n";
            File.AppendAllText(_path, dataRow);
        }

        public StaffEmployee GetEmployee(string lastName)
        {
            var data = File.ReadAllText(_path);
            var dataRows = data.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            StaffEmployee staffEmployee = null;

            foreach (var dataRow in dataRows)
            {
                if (dataRow.Contains(lastName))
                {
                    var dataMembers = dataRow.Split(_delimeter);
                    staffEmployee = new StaffEmployee(dataMembers[0],
                                                      decimal.TryParse(dataMembers[1], out var salary) ? salary : 0);
                    break;
                }
            }
            return staffEmployee;
        }
    }
}