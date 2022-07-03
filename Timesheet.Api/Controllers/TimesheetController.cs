using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Application.Services;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : Controller
    {
        private readonly ITimesheetService _timesheetService;

        public TimesheetController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpPost]
        public ActionResult<bool> TrackTime(TimeLog timeLog)
        {
            return Ok(_timesheetService.TrackTime(timeLog, timeLog.LastName));
        }
    }
}

