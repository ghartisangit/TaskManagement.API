using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.Contracts.Dtos;

public record TaskCreateDto(
    string Title,
    string Description,
    DateTime DueDate,
    string AssignedUserId);