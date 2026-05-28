using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Contracts.Dtos;

public record UserDto(
    string Id,
    string Name,
    string Email,
    Role Role,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
