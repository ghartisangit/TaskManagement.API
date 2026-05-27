using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Repositories.Interfaces;
using TaskManagement.Infrastructure.Persistance.Data;

namespace TaskManagement.Infrastructure.Persistance.Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await _context.SaveChangesAsync(ct);

    public void Dispose()
        => _context.Dispose();
}
