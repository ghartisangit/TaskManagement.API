using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.Contracts.Dtos;

public record AuthResponseDto(
    string AccessToken,
    string RefreshToken,
    UserDto User,
    DateTime AccessTokenExpiration);