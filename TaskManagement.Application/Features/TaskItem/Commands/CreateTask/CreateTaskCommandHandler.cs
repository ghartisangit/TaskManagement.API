using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;
using TaskManagement.Application.Repositories.Services;

namespace TaskManagement.Application.Features.TaskItem.Commands.CreateTask;

public sealed class CreateTaskCommandHandler (ITaskService _taskService, ICurrentUserService _currentUserService): IRequestHandler<CreateTaskCommand, TaskResponseDto>
{
    public async Task<TaskResponseDto> Handle(CreateTaskCommand request, CancellationToken ct)
    {
        
        var currentUserId = _currentUserService.UserId;

        var result = await _taskService.CreateTaskAsync(request.dto, currentUserId, ct);

        return result;
    }
}
