using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TaskManagement.Application.Repositories.Services;

namespace TaskManagement.Infrastructure.Persistance.Services;

public class CurrentUserService:ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?.User?
                .FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(claim, out var userId))
                return userId;

            throw new UnauthorizedAccessException("User is not authenticated.");
        }
    }

    public string? Email =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}
