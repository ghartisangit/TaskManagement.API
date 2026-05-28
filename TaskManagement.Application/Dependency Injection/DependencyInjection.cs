using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;
using TaskManagement.Application.Mapping;
using TaskManagement.Application.Validators;

namespace TaskManagement.Application.Dependency_Injection;

public static  class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<LoginValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
        services.AddValidatorsFromAssemblyContaining<TaskCreationValidator>();

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(TaskProfile).Assembly);
        services.AddSingleton(config);

        services.AddScoped<IMapper, ServiceMapper>();


        return services;
    }
}
