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

        public Issues[] Get(string expectedLogin, string expectedProject)
        {
            var issues = _client.Get(expectedLogin, expectedProject).Result;

            //map

            return issues;
        }
    }
}