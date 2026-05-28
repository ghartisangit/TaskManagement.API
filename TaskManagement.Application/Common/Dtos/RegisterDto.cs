using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.Common.Dtos;

public record RegisterDto(
    string Name,
    string Email,
    string Password);
