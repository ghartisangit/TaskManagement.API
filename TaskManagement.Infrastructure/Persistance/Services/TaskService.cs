using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;
using TaskManagement.Application.Common.Exceptions;
using TaskManagement.Application.Repositories.Interfaces;
using TaskManagement.Application.Repositories.Services;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistance.Services;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<TaskService> _logger;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork uow, ILogger<TaskService> logger, IMapper mapper)
    {
        _uow = uow;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<TaskResponseDto> CreateTaskAsync(TaskCreateDto taskCreateDto, Guid currentUserId, CancellationToken ct)
    {
        var manager = await _uow.UserRepository.GetByIdAsync(currentUserId, ct);
        if (manager == null)
        {
            _logger.LogWarning("User with ID {UserId} not found.", currentUserId);
            throw new KeyNotFoundException($"User with ID {currentUserId} not found.");
        }

        var task = new TaskItem
        (
            title: taskCreateDto.Title,
            description: taskCreateDto.Description,
            createdBy: manager,
            duedate: taskCreateDto.DueDate
        );

        if (!string.IsNullOrWhiteSpace(taskCreateDto.AssignedUserId))
        {
            if (!Guid.TryParse(taskCreateDto.AssignedUserId, out var developerGuid))
            {
                throw new BadRequestException("The provided assigned user ID format is invalid.");
            }

            var developer = await _uow.UserRepository.GetByIdAsync(developerGuid, ct);
            if (developer == null)
            {
                _logger.LogWarning("Task creation aborted: Assigned Developer ID {AssignedUserId} not found.", taskCreateDto.AssignedUserId);
                throw new NotFoundException(nameof(User), developerGuid);
            }

            task.AssignTo(developer);
        }

        try
        {
            await _uow.TaskRepository.CreateAsync(task, ct);
            await _uow.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while creating task.");
            throw new Exception("An error occurred while saving the task. Please try again later.");
        }
        return _mapper.Map<TaskResponseDto>(task);
    }

    public async Task<bool> DeleteTaskAsync(Guid id, Guid currentUserId, CancellationToken ct)
    {
        var task = await _uow.TaskRepository.GetByIdAsync(id, ct);
        if (task == null)
        {
            _logger.LogWarning("Task with ID {TaskId} not found for deletion.", id);
            throw new NotFoundException(nameof(TaskItem), id);
        }

        if(task.CreatedByManagerId != currentUserId)
        {
            _logger.LogWarning("Unauthorized deletion attempt: User {UserId} tried to delete task {TaskId} created by {CreatorId}.", currentUserId, id, task.CreatedByManagerId);
            throw new UnauthorizedAccessException("You are not authorized to delete this task.");
        }

        try
        {
            await _uow.TaskRepository.RemoveAsync(task, ct);
            await _uow.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while deleting task with ID {TaskId}.", id);
            throw new Exception("An error occurred while deleting the task. Please try again later.");
        }

        return true;
    }

    public async Task<IReadOnlyList<TaskResponseDto>> GetAllTaskAsync(int pageNumber, int pageSize, CancellationToken ct)
    {
        var tasks = await _uow.TaskRepository.GetPagedTasksAsync(pageNumber, pageSize, ct);
        var pagintedTasks = tasks.Items.Adapt<IReadOnlyList<TaskResponseDto>>();

        return pagintedTasks;
    }

    public async Task<TaskResponseDto> GetTaskByIdAsync(Guid id, CancellationToken ct)
    {
        var task = await _uow.TaskRepository.GetByIdAsync(id, ct);
        if (task == null)
        {
            _logger.LogWarning("Task with ID {TaskId} not found.", id);
            throw new NotFoundException(nameof(TaskItem), id);
        }

        return _mapper.Map<TaskResponseDto>(task);
    }

    public async Task<TaskResponseDto> UpdateTaskAsync(Guid id, UpdateTaskDto taskdto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
