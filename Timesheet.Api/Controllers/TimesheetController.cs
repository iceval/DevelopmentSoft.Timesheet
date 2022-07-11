using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.Models;
using Timesheet.BussinessLogic.Services;
using Timesheet.Domain;
using Timesheet.Domain.Models;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : Controller
    {
        private readonly ITimesheetService _timesheetService;
        private readonly IMapper _mapper;
        private readonly ILogger<TimesheetController> _logger;

        public TimesheetController(ITimesheetService timesheetService, IMapper mapper, ILogger<TimesheetController> logger)
        {
            _timesheetService = timesheetService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<bool> TrackTime(CreateTimeLogRequest request)
        {
            _logger.LogInformation("Пользователь фиксирует рабочее время" + JsonConvert.SerializeObject(request, Formatting.Indented));

            if (ModelState.IsValid)
            {
                var timeLog = _mapper.Map<TimeLog>(request);

                var result = _timesheetService.TrackTime(timeLog, timeLog.LastName);
                _logger.LogInformation("Пользователь успешно зафиксировал время");
                return Ok(result);
            }

            return BadRequest();
        }
    }
}