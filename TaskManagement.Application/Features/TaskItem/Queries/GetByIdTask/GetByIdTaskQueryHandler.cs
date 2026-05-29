using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;
using TaskManagement.Application.Repositories.Services;

namespace TaskManagement.Application.Features.TaskItem.Queries.GetByIdTask;

public record GetByIdTaskQuery(Guid Id) : IRequest<TaskResponseDto>;

public class GetByIdTaskQueryValidator : AbstractValidator<GetByIdTaskQuery>
{
    public GetByIdTaskQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Task ID is required.")
            .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Task ID must be a valid GUID.");
    }
}
public sealed class GetByIdTaskQueryHandler(ITaskService _taskService) : IRequestHandler<GetByIdTaskQuery, TaskResponseDto>
{
    public async Task<TaskResponseDto> Handle(GetByIdTaskQuery request, CancellationToken cancellationToken)
    {
        return await _taskService.GetTaskByIdAsync(request.Id, cancellationToken);
    }
}
