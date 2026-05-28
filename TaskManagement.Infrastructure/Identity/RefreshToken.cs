using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TaskManagement.Domain.Common;

namespace TaskManagement.Infrastructure.Identity;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public string Token { get; private set; } = default!;
    public DateTime ExpiresAt { get; private set; }

    public string CreatedByIp { get; private set; } = default!;
    public DateTime? RevokedAt { get; private set; }
    public string? RevokedByIp { get; private set; }
    public string? ReplacedByToken { get; private set; }

    public bool IsExpird =>  DateTime.UtcNow >= ExpiresAt;
    public bool IsRevoked => RevokedAt != null;
    public bool IsActive => !IsRevoked && !IsExpird;

   public Guid AppUserId { get; private set; }
    public AppUser AppUser { get; private set; } = null!;

    private RefreshToken() { }

    public RefreshToken(string token, DateTime expiresAt, string createdByIp, Guid userId)
    {
        if(string.IsNullOrWhiteSpace(token))
            throw new ArgumentNullException(nameof(token));

        if(expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("ExpiresAt must be in the future", nameof(expiresAt));

        if(string.IsNullOrWhiteSpace(createdByIp))
            throw new ArgumentNullException(nameof(createdByIp));

        if(userId == Guid.Empty)
            throw new ArgumentNullException("User can't be empty", nameof(userId));

        Token = token;
        ExpiresAt = expiresAt;
        CreatedByIp = createdByIp;
        AppUserId = userId;
    }

    public void Revoke(string ipAddress, string? replacedByToken = null)
    {
        if(!IsActive)
            throw new InvalidOperationException("Only active tokens can be revoked.");

        RevokedAt = DateTime.UtcNow;
        RevokedByIp = string.IsNullOrWhiteSpace(ipAddress) ? "Unknown" : ipAddress;
        ReplacedByToken = replacedByToken;
    }
}
