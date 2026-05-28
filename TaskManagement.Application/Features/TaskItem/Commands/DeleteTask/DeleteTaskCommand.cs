using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.Features.TaskItem.Commands.DeleteTask;

public record DeleteTaskCommand(Guid id) : IRequest<bool>;


public class DeleteTaskValidator: AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskValidator()
    {
        RuleFor(x => x.id)
            .NotEmpty().WithMessage("Task ID is required.");
    }
}