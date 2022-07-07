using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheet.Domain.Models;

namespace Timesheet.DataAccess.MSSQL
{
    public class DataAccessMappingProfile : Profile
    {
        public DataAccessMappingProfile()
        {
            CreateMap<Employee, Entities.Employee>()
                .ReverseMap();

            CreateMap<ChiefEmployee, Entities.Employee>()
                .IncludeBase<Employee, Entities.Employee>()
                .ReverseMap();

            CreateMap<StaffEmployee, Entities.Employee>()
                .IncludeBase<Employee, Entities.Employee>()
                .ReverseMap();

            CreateMap<FreelancerEmployee, Entities.Employee>()
                .IncludeBase<Employee, Entities.Employee>()
                .ReverseMap();
        }
    }
}