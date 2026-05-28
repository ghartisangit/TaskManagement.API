using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.Common.Dtos;

public record TaskResponseDto(
    Guid Id,
    string Title,
    string Description,
    DateTime DueDate,
    string Status,
    string CreatedByUserId,
    string CreatedByUserName,
    string? AssignedUserId,
    string? AssignedUserName,
    DateTime CreatedAt,
    DateTime? UpdatedAt);