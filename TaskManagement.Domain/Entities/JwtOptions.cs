using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskManagement.Domain.Entities;

public class JwtOptions
{
    public const string SectionName = "JwtSettings";

    [Required(AllowEmptyStrings =false)]
    [MinLength(32, ErrorMessage = "Key must be at least 32 characters long.")]
    public string Key { get; init; } = string.Empty;

    [Required]
    public string Issuer { get; init; } = string.Empty;

    [Required]
    public string Audience { get; init; } = string.Empty;

    [Required]
    [AllowedValues(1, 1440, ErrorMessage = "AccessTokenExpiryMinutes must be between 1 and 1440.")]
    public int AccessTokenExpiryMinutes { get; init; }

    public int RefreshTokenExpiryDays { get; init; } = 7;
}
