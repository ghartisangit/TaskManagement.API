using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;

namespace TaskManagement.Application.Repositories.Services;

public interface ITaskService
{
    Task<IReadOnlyList<TaskResponseDto>> GetAllTaskAsync(int pageNumber, int pageSize, CancellationToken ct = default);
    Task<TaskResponseDto> GetTaskByIdAsync(Guid id, CancellationToken ct = default);
    Task<TaskResponseDto> CreateTaskAsync(TaskCreateDto taskCreateDto,Guid currentUserId, CancellationToken ct = default);
    Task<TaskResponseDto> UpdateTaskAsync(Guid id, UpdateTaskDto taskdto, CancellationToken ct = default);
    Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct = default);
}
