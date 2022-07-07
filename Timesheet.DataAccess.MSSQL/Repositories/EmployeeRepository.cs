using AutoMapper;
using System;
using System.Linq;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.DataAccess.MSSQL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TimesheetContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(TimesheetContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add(Employee employee)
        {
            var newEmployee = _mapper.Map<Entities.Employee>(employee);

            //var newEmployee = new Entities.Employee
            //{
            //    LastName = employee.LastName,
            //    Position = employee.Position,
            //    Salary = employee.Salary
            //};

            //if (employee is ChiefEmployee chief)
            //{
            //    newEmployee.Bonus = chief.Bonus;
            //}

            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
        }

        public Employee Get(string lastName)
        {
            var employee = _context.Employees
                .FirstOrDefault(x => x.LastName.ToLower() == lastName.ToLower());

            if (employee == null)
            {
                return null;
            }

            switch (employee.Position)
            {
                case Position.Chief:
                    return _mapper.Map<ChiefEmployee>(employee);
                //var bonus = employee.Bonus ?? 0m;
                //return new ChiefEmployee(employee.LastName, employee.Salary, bonus);
                case Position.Staff:
                    return _mapper.Map<StaffEmployee>(employee);
                //return new StaffEmployee(employee.LastName, employee.Salary);
                case Position.Freelancer:
                    return _mapper.Map<FreelancerEmployee>(employee);
                //return new FreelancerEmployee(employee.LastName, employee.Salary);
                default:
                    throw new Exception("Wrong position" + employee.Position);
            }

            //return new Employee
            //{
            //    LastName = employee.LastName,
            //    Position = employee.Position,
            //    Salary = employee.Salary
            //};
        }
    }
}
