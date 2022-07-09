using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.Models;
using Timesheet.Domain.Models;

namespace Timesheet.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<CreateTimeLogRequest, TimeLog>();
            CreateMap<EmployeeReport, GetEmployeeReportResponse>();
            CreateMap<TimeLog, TimeLogDto>();
            CreateMap<Issues, IssueDto>();
        }
    }
}