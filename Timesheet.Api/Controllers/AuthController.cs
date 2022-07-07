using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Api.ResourceModels;
using Timesheet.BussinessLogic.Exceptions;
using Timesheet.BussinessLogic.Services;
using Timesheet.Domain;

namespace Timesheet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public ActionResult<bool> Login(LoginRequest request)
        {
            try
            {
                var token = _authService.Login(request.LastName);

                return Ok(_authService.Login(request.LastName));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}