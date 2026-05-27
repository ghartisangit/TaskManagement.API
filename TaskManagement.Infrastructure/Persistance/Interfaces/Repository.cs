using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Repositories.Interfaces;
using TaskManagement.Domain.Common;
using TaskManagement.Infrastructure.Persistance.Data;

namespace TaskManagement.Infrastructure.Persistance.Interfaces;

public class Repository<T>: IRepository<T> where T: BaseEntity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _dbSet.FindAsync(id, ct);

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
        => await _dbSet.AsNoTracking().ToListAsync(ct);

    public async Task CreateAsync(T entity, CancellationToken ct = default)
        => await _dbSet.AddAsync(entity, ct);

    public async Task UpdateAsync(T entity, CancellationToken ct = default)
        => _dbSet.Update(entity);

    public async Task RemoveAsync(T entity, CancellationToken ct = default)
        => _dbSet.Remove(entity) ;
}
