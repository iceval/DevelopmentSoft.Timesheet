using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet.Api.Models
{
    public class GetIssuesResponse
    {
        public IssueDto[] Issues { get; set; }
    }
}