using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;
using TaskManagement.Application.Repositories.Services;

namespace TaskManagement.Application.Features.TaskItem.Commands.CreateTask;

public sealed class CreateTaskCommandHandler (ITaskService _taskService): IRequestHandler<CreateTaskCommand, TaskResponseDto>
{
    public async Task<TaskResponseDto> Handle(CreateTaskCommand request, CancellationToken ct)
    {
        

        throw new NotImplementedException();
    }
}
