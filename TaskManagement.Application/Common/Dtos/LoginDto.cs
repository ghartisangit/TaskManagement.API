using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.Common.Dtos;

public record LoginDto(
    string Email,
    string Password);
