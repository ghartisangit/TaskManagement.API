using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Contracts.Dtos;
using TaskManagement.Application.Validators;

namespace TaskManagement.Application.Dependency_Injection;

public static  class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<LoginValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
        services.AddValidatorsFromAssemblyContaining<TaskCreationValidator>();


        return services;
    }
}
