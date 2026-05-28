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
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public Role Role { get; private set; }
    
    public ICollection<TaskItem> ManagedTask { get; private set; } = new List<TaskItem>();
    public ICollection<TaskItem> AssignedTask { get; private set; } = new List<TaskItem>();
    public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();


    private User() { }

    public User(string name, string email, string password, Role role)
    {
        Validate(name, email, role);
        Name = name;
        Email = email;
        PasswordHash = password;
        Role = role;
    }

    public void Update(string name, string email, Role role)
    {
        Validate(name, email,  role);
        Name = name;
        Email = email;
        Role = role;
        UpdateTimeStamp();
    }
    public void UpdatePasswordHash(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentNullException(nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
        UpdateTimeStamp();
    }

    public void Validate(string name, string email,Role role)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        if(string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            throw new ArgumentException("Email must be valid", nameof(email));


        if(!Enum.IsDefined(typeof(Role), role))
            throw new ArgumentException("Invalid role", nameof(role));
    }

}
