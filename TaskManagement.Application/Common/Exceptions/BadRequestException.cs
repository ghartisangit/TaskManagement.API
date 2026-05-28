using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Application.Common.Exceptions;

public class BadRequestException: TaskManagementException
{
    public BadRequestException(string message) : base(message) { }
}
