using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.TaskItem.Commands.UpdateTask;

public record UpdateTaskCommand(
    Guid id,
     string Title,
    string Description,
    DateTime DueDate,
    string AssignedUserId,
    TaskStatuss Status);
