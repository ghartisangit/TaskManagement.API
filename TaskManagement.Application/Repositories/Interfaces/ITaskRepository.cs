using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Pagination;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Repositories.Interfaces;

public interface ITaskRepository: IRepository<TaskItem>
{
    Task<PaginatedList<TaskItem>> GetPagedTasksAsync(int pageNumber, int pageSize, CancellationToken ct = default);
}
