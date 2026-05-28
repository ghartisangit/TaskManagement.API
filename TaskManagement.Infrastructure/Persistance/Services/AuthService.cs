using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Contracts.Dtos;
using TaskManagement.Application.Repositories.Interfaces;
using TaskManagement.Application.Repositories.Services;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistance.Services;

public class AuthService : IAuthService
{
    private readonly JwtOptions jwtOptions;
    private readonly IUnitOfWork _uow;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthService> _logger;

    public AuthService(JwtOptions jwtOptions, IUnitOfWork uow, IHttpContextAccessor httpContextAccessor, ILogger<AuthService> logger)
    {
        this.jwtOptions = jwtOptions;
        _uow = uow;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto request, string ipAddress, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(string token, string ipAddress, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

   
    public async Task RevokeTokenAsync(string token, string ipAddress, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
