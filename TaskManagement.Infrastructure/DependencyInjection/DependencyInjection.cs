using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Repositories.Interfaces;
using TaskManagement.Application.Repositories.Services;
using TaskManagement.Infrastructure.Identity;
using TaskManagement.Infrastructure.Persistance.Data;
using TaskManagement.Infrastructure.Persistance.Interfaces;
using TaskManagement.Infrastructure.Persistance.Services;

namespace TaskManagement.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentityCore<AppUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        }).AddRoles<IdentityRole<Guid>>()
          .AddEntityFrameworkStores<AppDbContext>();


        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITaskService, TaskService>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}
