using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using TaskManagement.Domain.Common;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities;

public class User: BaseEntity
{
    public string Name { get; private set; } = default!;
    public Role Role { get; private set; }
    
    public ICollection<TaskItem> ManagedTask { get; private set; } = new List<TaskItem>();
    public ICollection<TaskItem> AssignedTask { get; private set; } = new List<TaskItem>();


    private User() { }

    public User(Guid id, string name, Role role)
    {
        Validate(name, role);
        Id = id;
        Name = name;
        Role = role;
    }

    public void Update(string name,Role role)
    {
        Validate(name,   role);
        Name = name;
        Role = role;
        UpdateTimeStamp();
    }
 

    public static void Validate(string name,Role role)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        if(!Enum.IsDefined<Role>(role))
            throw new ArgumentException("Invalid role", nameof(role));
    }

}
