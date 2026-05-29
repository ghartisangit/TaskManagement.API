using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagement.Application.Common.Dtos;
using TaskManagement.Application.Repositories.Interfaces;
using TaskManagement.Application.Repositories.Services;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Infrastructure.Identity;

namespace TaskManagement.Infrastructure.Persistance.Services;

public class AuthService : IAuthService
{
    private readonly JwtOptions jwtOptions;
    private readonly IUnitOfWork _uow;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthService> _logger;
    private readonly UserManager<AppUser> _userManager;

    public AuthService(IOptions<JwtOptions> jwtOptions, IUnitOfWork uow, IHttpContextAccessor httpContextAccessor, ILogger<AuthService> logger, UserManager<AppUser> userManager)
    {
        this.jwtOptions = jwtOptions.Value;
        _uow = uow;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto request,Role role, CancellationToken ct)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if(existingUser != null)
        {
            _logger.LogWarning($"User with this {request.Email} is already registered");
            throw new InvalidOperationException($"A user with this mail is already registered {request.Email}");
        }

        var appUser = new AppUser
        {
            Id = Guid.NewGuid(),
            UserName = request.Name.Trim(),
            Email = request.Email.Trim(),
            NormalizedEmail = request.Email.Trim().ToUpperInvariant(),
            NormalizedUserName = request.Email.Trim().ToUpperInvariant(),
            EmailConfirmed = true
        };

        var identityResult = await _userManager.CreateAsync(appUser, request.Password);
        if(!identityResult.Succeeded)
        {
            var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
            _logger.LogError($"Failed to register user with email {request.Email}. Errors: {errors}");
            throw new InvalidOperationException($"Failed to register user. Errors: {errors}");
        }
        await _userManager.AddToRoleAsync(appUser, role.ToString());

        var domainUser = new User(appUser.Id, appUser.UserName, role);
        await _uow.UserRepository.CreateAsync(domainUser, ct);
        await _uow.SaveChangesAsync();

        var accessToken = GenerateAccessToken(domainUser, appUser);
        var refreshToken = GenerateRefreshToken(ct);

        var refreshTokenEntity = new RefreshToken(
        token: refreshToken,
        expiresAt: DateTime.UtcNow.AddDays(jwtOptions.RefreshTokenExpiryDays),
        createdByIp: GetIpAddress(),
        userId: appUser.Id
             );

        var expiretime = DateTime.UtcNow.AddMinutes(jwtOptions.AccessTokenExpiryMinutes);

        appUser.RefreshTokens.Add(refreshTokenEntity);
        await _userManager.UpdateAsync(appUser);
        await _uow.SaveChangesAsync(ct);

        return new AuthResponseDto(accessToken, refreshToken, new UserDto(
            appUser.Id.ToString(),
            domainUser.Name,
            appUser.Email,
            domainUser.Role,
            domainUser.CreatedAt,
            domainUser.UpdatedAt
        )
       , expiretime);

    }
    private string GetIpAddress()
    {
        return _httpContextAccessor.HttpContext?.Request?.Headers["X-Forwarded-For"].FirstOrDefault()
                            ?? _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString()
                            ?? "Unknown";
    }

    public  string GenerateAccessToken(User user, AppUser appUser)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub,   appUser.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, appUser.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString()),
            new(ClaimTypes.Name,               user.Name),
            new(ClaimTypes.Role,               user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtOptions.AccessTokenExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string GenerateRefreshToken(CancellationToken ct = default)
    {
        var randomBytes = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
        return Convert.ToBase64String(randomBytes);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto request, string ipAddress, CancellationToken ct)
    {
        var appUser = await _userManager.FindByEmailAsync(request.Email);
        if(appUser == null || !await _userManager.CheckPasswordAsync(appUser, request.Password))
        {
            _logger.LogWarning($"Invalid login attempt for email {request.Email}");
            throw new UnauthorizedAccessException("Invalid email or password.");
        }
        var domainUser = await _uow.UserRepository.GetByIdAsync(appUser.Id, ct)
           ?? throw new InvalidOperationException("Domain user record not found.");

        var accessToken = GenerateAccessToken(domainUser, appUser);
        var refreshToken = GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken(
            token: refreshToken,
            expiresAt: DateTime.UtcNow.AddDays(jwtOptions.RefreshTokenExpiryDays),
            createdByIp: GetIpAddress(),
            userId: appUser.Id
        );

        await _uow.RefreshTokenRepository.CreateAsync(refreshTokenEntity, ct);
        await _uow.SaveChangesAsync(ct);

        return new AuthResponseDto(accessToken, refreshToken, new UserDto(
            appUser.Id.ToString(),
            domainUser.Name,
            appUser.Email,
            domainUser.Role,
            domainUser.CreatedAt,
            domainUser.UpdatedAt
        )
       , expiretime);
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
