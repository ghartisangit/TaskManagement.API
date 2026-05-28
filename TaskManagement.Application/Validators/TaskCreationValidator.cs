using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;

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
            .NotEmpty().WithMessage("Due date is required.");
            
        RuleFor(x => x.AssignedUserId)
            .Must(id => string.IsNullOrEmpty(id) || Guid.TryParse(id, out _))
            .WithMessage("Assigned User ID must be a valid GUID.");
    }
}
