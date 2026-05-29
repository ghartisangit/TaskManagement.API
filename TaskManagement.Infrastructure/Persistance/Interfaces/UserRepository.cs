using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Repositories.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Identity;
using TaskManagement.Infrastructure.Persistance.Data;

namespace TaskManagement.Infrastructure.Persistance.Interfaces;

public class UserRepository: Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
   

    public async Task<User?> GetUserWithTasksByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbSet.Include(u => u.ManagedTask)
                            .Include(u=> u.AssignedTask)
                            .FirstOrDefaultAsync(u => u.Id == id, ct);
    }

}
