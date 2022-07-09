using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<JwtConfig> _jwtconfig;

        public AuthController(IAuthService authService, IOptions<JwtConfig> jwtconfig)
        {
            _authService = authService;
            _jwtconfig = jwtconfig;
        }

        [HttpPost]
        public ActionResult<bool> Login(LoginRequest request)
        {
            try
            {
                var secret = _jwtconfig.Value.Secret;
                var token = _authService.Login(request.LastName, secret);

                return Ok(token);
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