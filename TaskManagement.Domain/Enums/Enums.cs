using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Domain.Enums;

public enum Role
{
    Admin = 1,
    Manager = 2,
    Developer = 3
}

public enum TaskStatuss
{
    UnAssigned = 1,
    Assigned = 2,
    InProgress = 3,
    Completed = 4
}