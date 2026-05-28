using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Common.Dtos;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Repositories.Interfaces;

public interface IUserRepository:IRepository<User>
{
    Task<IReadOnlyList<User>> GetByEmailAsync(string email, CancellationToken ct = default);
    Task<User?> GetUserWithTasksByIdAsync(Guid id, CancellationToken ct = default);
}
