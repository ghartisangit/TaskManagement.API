using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    public void UpdateTimeStamp() => UpdatedAt = DateTime.UtcNow;
}
