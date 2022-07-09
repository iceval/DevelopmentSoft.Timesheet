using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet.Api.Models
{
    public class CreateTimeLogRequest
    {
        public DateTime Date { get; set; }
        public int WorkingHours { get; set; }
        public string LastName { get; set; }
        public string Comment { get; set; }
    }

    /*public class TimeLogValidator
    {
        private readonly CreateTimeLogRequest _timeLog;

        public TimeLogValidator(CreateTimeLogRequest timeLog)
        {
            _timeLog = timeLog;
        }

        public (bool isValid, IEnumerable<ValidationResult> validationResults) Validate()
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_timeLog);
            var isValid = Validator.TryValidateObject(_timeLog, validationContext, validationResults);

            isValid = _timeLog.Date <= DateTime.Now && _timeLog.Date > _timeLog.Date.AddYears(-1)
                && isValid;

            isValid = _timeLog.WorkingHours > 0
                && _timeLog.WorkingHours <= 24
                && !string.IsNullOrWhiteSpace(_timeLog.LastName) && isValid;

            return (isValid, validationResults);
        }
    }*/

    public class TimeLogFluentValidator : AbstractValidator<CreateTimeLogRequest>
    {
        public TimeLogFluentValidator()
        {
            RuleFor(x => x.Date)
                .GreaterThan(x => x.Date.AddYears(-1))
                .LessThan(DateTime.Now);

            RuleFor(x => x.WorkingHours)
                .GreaterThan(0)
                .LessThanOrEqualTo(24);

            RuleFor(x => x.LastName)
                .NotEmpty();

            //var validationResults = new List<ValidationResult>();
            //var validationContext = new ValidationContext(_timeLog);

            //var isValid = Validator.TryValidateObject(_timeLog, validationContext, validationResults);

            //isValid = _timeLog.Date <= DateTime.Now && _timeLog.Date > _timeLog.Date.AddYears(-1)
            //    && isValid;
        }
    }
}