using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.BussinessLogic.Services
{
    public class IssuesService : IIssuesService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IIssuesClient _client;

        public IssuesService(IEmployeeRepository employeeRepository, IIssuesClient client)
        {
            _employeeRepository = employeeRepository;
            _client = client;
        }

        /*
         3) Получаем информацию о пользователе (роль и фамилия)
         4) Получаем список задач из системы с задачами
         5) Возвращаем список задач
         */

        public Issues[] Get()
        {
            //
            var issues = _client.Get("iceval").Result;

            //map

            return issues;
        }
    }
}