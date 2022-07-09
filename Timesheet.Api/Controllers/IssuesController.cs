using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.Models;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : Controller
    {
        private readonly IIssuesService _issuesService;
        private readonly IMapper _mapper;

        public IssuesController(IIssuesService issuesService, IMapper mapper)
        {
            _issuesService = issuesService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<GetIssuesResponse> Get()
        {
            var issues = _issuesService.Get("iceval", "Timesheets");

            return new GetIssuesResponse
            {
                Issues = _mapper.Map<IssueDto[]>(issues)
            };
        }
    }
}