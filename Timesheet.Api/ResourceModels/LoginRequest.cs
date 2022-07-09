using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet.Api.ResourceModels
{
    public class LoginRequest
    {
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