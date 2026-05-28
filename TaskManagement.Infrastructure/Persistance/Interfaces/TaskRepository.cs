using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Pagination;
using TaskManagement.Application.Repositories.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Persistance.Data;

namespace TaskManagement.Infrastructure.Persistance.Interfaces;

public class TaskRepository : Repository<TaskItem>, ITaskRepository
{
    public TaskRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PaginatedList<TaskItem>> GetPagedTasksAsync(int pageNumber, int pageSize, CancellationToken ct)
    {
        var query = _dbSet.Include(t => t.CreatedByManager)
            .Include(t => t.AssignedToDeveloper)
            .AsNoTracking()
            .OrderByDescending(t => t.CreatedAt);

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PaginatedList<TaskItem>(items, totalCount, pageNumber, pageSize);

    }
}
