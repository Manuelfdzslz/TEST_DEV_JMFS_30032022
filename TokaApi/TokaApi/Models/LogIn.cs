using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaApi.Models
{
    public class LogIn
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public class LogInValidator : AbstractValidator<LogIn>
    {
        public LogInValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
