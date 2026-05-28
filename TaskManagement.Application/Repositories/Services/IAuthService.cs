using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Contracts.Dtos;

namespace TaskManagement.Application.Repositories.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto request, CancellationToken ct = default); 
    Task<AuthResponseDto> LoginAsync(LoginDto request,string ipAddress, CancellationToken ct = default);
    Task<AuthResponseDto> RefreshTokenAsync(string token, string ipAddress, CancellationToken ct = default);
    Task RevokeTokenAsync(string token, string ipAddress, CancellationToken ct = default);
}
