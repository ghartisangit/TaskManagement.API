using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Identity;

public class AppUser:IdentityUser<Guid>
{
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
