using Microsoft.AspNetCore.Mvc;
using Timesheet.Domain.Models;
using Timesheet.Domain;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : Controller
    {
        private readonly ITimesheetService _timeSheetService;

        public TimesheetController(ITimesheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }

        [HttpPost]
        public ActionResult<bool> TrackTime(TimeLog timeLog)
        {
            return Ok(_timeSheetService.TrackTime(timeLog));
        }
    }
}

