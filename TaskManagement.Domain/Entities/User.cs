using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TaskManagement.Domain.Common;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities;

public class User: BaseEntity
{
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;
    public Role Role { get; private set; }
    public ICollection<TaskItem> ManagedTask { get; private set; } = new List<TaskItem>();
    public ICollection<TaskItem> AssignedTask { get; private set; } = new List<TaskItem>();


    private User() { }

    public User(string name, string email, string password, Role role)
    {
        Validate(name, email, password, role);
        Name = name;
        Email = email;
        Password = password;
        Role = role;
    }

    public void Update(string name, string email, string password, Role role)
    {
        Validate(name, email, password, role);
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        UpdateTimeStamp();
    }

    public void Validate(string name, string email, string password, Role role)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        
        if(string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        if(!email.Contains("@") || !email.Contains("."))
            throw new ArgumentException("Email must be valid", nameof(email));

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));

        if (password.Length < 6)
            throw new ArgumentException("Password must be at least 6 characters long", nameof(password));
        
        if(!Enum.IsDefined(typeof(Role), role))
            throw new ArgumentException("Invalid role", nameof(role));
    }
}
