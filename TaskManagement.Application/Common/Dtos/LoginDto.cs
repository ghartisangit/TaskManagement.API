using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.Contracts.Dtos;

public record LoginDto(
    string Email,
    string Password);
