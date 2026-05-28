using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.Repositories.Interfaces;

public interface IUnitOfWork:IDisposable
{
    ITaskRepository TaskRepository { get; }
    IUserRepository UserRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
