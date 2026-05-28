using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.Common.Exceptions;

public abstract class TaskManagementException: Exception
{
    protected TaskManagementException(string message) : base(message) { }
    protected TaskManagementException(string message, Exception inner) : base(message, inner) { }
}
