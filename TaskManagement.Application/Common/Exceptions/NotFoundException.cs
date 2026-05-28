using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.Common.Exceptions;

public class NotFoundException: TaskManagementException
{
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string entityName, object key) : base($"{entityName} with key '{key}' was not found.") { }
}
