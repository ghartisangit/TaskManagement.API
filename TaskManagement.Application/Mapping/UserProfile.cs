using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Mapping;

public class UserProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Role, src => src.Role.ToString())
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.UpdatedAt, src => src.UpdatedAt);
    }
}
