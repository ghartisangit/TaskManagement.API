using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Common;

namespace TaskManagement.Application.Repositories.Interfaces;

public interface IRepository<T> where T: BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
    Task CreateAsync(T entity, CancellationToken ct = default);
    Task UpdateAsync(T entity, CancellationToken ct = default);
    Task RemoveAsync(T entity, CancellationToken ct = default);
}
