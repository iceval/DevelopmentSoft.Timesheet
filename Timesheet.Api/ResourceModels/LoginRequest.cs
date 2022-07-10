using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet.Api.ResourceModels
{
    /// <summary>
    /// LoginRequest to auth in api
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// User's Last Name
        /// </summary>
        [Required]
        public string LastName { get; set; }
    }

    public class LoginRequestFluentValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestFluentValidator()
        {
            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }
}