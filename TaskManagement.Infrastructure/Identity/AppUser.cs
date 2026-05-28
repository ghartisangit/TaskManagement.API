using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Infrastructure.Identity;

public class AppUser:IdentityUser<Guid>
{
}
