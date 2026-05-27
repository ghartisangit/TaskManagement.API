using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Contracts.Dtos;

namespace TaskManagement.Application.Validators;

public class RegisterValidator:AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {

        RuleFor(x=> x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}
