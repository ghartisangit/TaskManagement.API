using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;

namespace TaskManagement.Application.Features.TaskItem.Commands.CreateTask;

public record CreateTaskCommand(TaskCreateDto dto) : IRequest<TaskResponseDto>;

public class CreateTaskValidator: AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.dto.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");
        RuleFor(x => x.dto.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        RuleFor(x => x.dto.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.");

        RuleFor(x => x.dto.AssignedUserId)
            .Must(id => string.IsNullOrEmpty(id) || Guid.TryParse(id, out _))
            .WithMessage("Assigned User ID must be a valid GUID.");
    }
}

