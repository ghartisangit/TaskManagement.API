using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Mapping;

public class TaskProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TaskItem, TaskResponseDto>()
            .Map(dest=> dest.Id, src=> src.Id)
            .Map(dest=> dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.DueDate, src => src.DueDate)
            .Map(dest => dest.Status, src => src.Status.ToString())
            .Map(dest => dest.CreatedByUserId, src => src.CreatedByManagerId.ToString())
            .Map(dest => dest.CreatedByUserName, src => src.CreatedByManager.Name)
            .Map(dest => dest.AssignedUserId, src => src.AssignedToDeveloperId)
            .Map(dest=> dest.AssignedUserName, src => src.AssignedToDeveloper )
            .Map(dest=> dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.UpdatedAt, src => src.UpdatedAt);
    }
}
