using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Common.Dtos;

public record UpdateTaskDto(
    string Title,
    string Description,
    DateTime DueDate,
    string AssignedUserId,
    TaskStatuss Status);
