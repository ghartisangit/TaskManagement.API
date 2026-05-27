using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Contracts.Dtos;

namespace TaskManagement.Application.Validators;

public class TaskCreationValidator: AbstractValidator<TaskCreateDto>
{
    public TaskCreationValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("Due date is required.")
            .GreaterThan(DateTime.Now).WithMessage("Due date must be in the future.");

        RuleFor(x => x.AssignedUserId)
            .NotEmpty().WithMessage("Assigned user ID is required.");
    }
}
