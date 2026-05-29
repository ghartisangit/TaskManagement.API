using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;
using TaskManagement.Application.Repositories.Services;

namespace TaskManagement.Application.Features.TaskItem.Queries.GetAllTask;

public record GetAllTaskQuery(int pageNumber, int pageSize) : IRequest<IReadOnlyList<TaskResponseDto>>;


public class GetAllTaskQueryValidator : AbstractValidator<GetAllTaskQuery>
{
    public GetAllTaskQueryValidator()
    {
        RuleFor(x => x.pageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");
        RuleFor(x => x.pageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100.");
    }
}

public sealed class GetAllTaskQueryHandler(ITaskService _taskService) : IRequestHandler<GetAllTaskQuery, IReadOnlyList<TaskResponseDto>>
{
    public async Task<IReadOnlyList<TaskResponseDto>>Handle(GetAllTaskQuery request, CancellationToken cancellationToken)
    {
        return await _taskService.GetAllTaskAsync(request.pageNumber, request.pageSize, cancellationToken);
    }
}
