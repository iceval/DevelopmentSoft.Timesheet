using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public AuthService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public bool Login(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return false;
            }

            Employee employee = _employeeRepository.GetEmployee(lastName);
            var isEmployeeExist = employee != null;

            if (isEmployeeExist)
                UserSession.Sessions.Add(lastName);

            return isEmployeeExist;
        }

        public static class UserSession
        {

            public static HashSet<string> Sessions { get; set; } = new HashSet<string>();
        }
    }
}
