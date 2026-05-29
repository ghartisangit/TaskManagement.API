using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Repositories.Services;

namespace TaskManagement.Application.Features.TaskItem.Commands.DeleteTask;

public sealed class DeleteTaskCommandHandler(ITaskService _service, ICurrentUserService _currentUserService) : IRequestHandler<DeleteTaskCommand, bool>
{
    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;
        var result = await _service.DeleteTaskAsync(request.id, currentUserId, cancellationToken);
        return result;
    }
}

