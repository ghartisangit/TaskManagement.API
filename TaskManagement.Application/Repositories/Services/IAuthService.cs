using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Repositories.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto request, Role role, CancellationToken ct = default); 
    Task<AuthResponseDto> LoginAsync(LoginDto request,string ipAddress, CancellationToken ct = default);
    Task<AuthResponseDto> RefreshTokenAsync(string token, string ipAddress, CancellationToken ct = default);
    Task RevokeTokenAsync(string token, string ipAddress, CancellationToken ct = default);
}
