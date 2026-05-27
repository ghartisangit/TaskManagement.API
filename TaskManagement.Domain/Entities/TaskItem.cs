using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Common;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities;

public class TaskItem: BaseEntity
{
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public TaskStatuss Status { get; private set; } = TaskStatuss.UnAssigned;

    public Guid CreatedByManagerId { get; private set; }
    public User CreatedByManager { get; private set; } = default!;

    public Guid? AssignedToDeveloperId { get; private set; }
    public User? AssignedToDeveloper { get; private set; }

    private TaskItem() { }

    public TaskItem(string title, string description, User createdBy)
    {
        if (createdBy.Role != Role.Manager)
            throw new InvalidOperationException("Only Manager can create a task");

        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException("title");

        if(string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException("description");

        Title = title;
        Description = description;
        CreatedByManager = createdBy;
        CreatedByManagerId = createdBy.Id;
        Status = TaskStatuss.UnAssigned;
    }

    public void AssignTo(User developer)
    {
        if (developer.Role != Role.Developer)
            throw new InvalidOperationException("Only developer can be assigned tasks");

        AssignedToDeveloper = developer;
        AssignedToDeveloperId = developer.Id;
        Status = TaskStatuss.Assigned;
        UpdateTimeStamp();
    }

    public void MarkAsCompleted()
    {
        if (Status != TaskStatuss.Assigned)
            throw new InvalidOperationException("Only assigned task can be completed");

        Status = TaskStatuss.Completed;
        UpdateTimeStamp();
    }
}
