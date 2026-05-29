using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Repositories.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Infrastructure.Identity;

namespace TaskManagement.Infrastructure.Persistance.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        await SeedAdminAsync(services);
        await SeedRoleAsync(services);
    }

    private static async Task SeedAdminAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        foreach(var roleName in Enum.GetNames<Role>())
        {
            if(!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }
    }

    private static async Task SeedRoleAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var uow = services.GetRequiredService<IUnitOfWork>();

        const string adminEmail = "admin@gmail.com";
        const string adminPassword = "Admin@123";
        const string adminName = "admin";

        if (await userManager.FindByEmailAsync(adminEmail) is not null)
            return;
        var adminId = Guid.NewGuid();

        var adminuser = new AppUser
        {
            Email = adminEmail,
            UserName = adminEmail,
            Id = adminId,
            NormalizedEmail = adminEmail.ToUpperInvariant(),
            NormalizedUserName = adminEmail.ToUpperInvariant(),
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminuser);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Admin seeding failed: {errors}");
        }


        await userManager.AddToRoleAsync(adminuser, Role.Admin.ToString());


        var domainUser = new User(adminId, adminName, Role.Admin);
        await uow.UserRepository.CreateAsync(domainUser);
        await uow.SaveChangesAsync();
    }
}
