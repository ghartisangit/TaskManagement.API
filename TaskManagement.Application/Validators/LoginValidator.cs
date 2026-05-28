using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;

namespace TaskManagement.Application.Validators;

public class LoginValidator: AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}
