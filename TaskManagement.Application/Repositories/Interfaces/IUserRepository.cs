using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Contracts.Dtos;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Repositories.Interfaces;

public interface IUserRepository:IRepository<User>
{
    Task<IReadOnlyList<UserDto>> GetByEmailAsync(string email, CancellationToken ct = default);
}
