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

    public DateTime DueDate { get; private set; }

    public Guid CreatedByManagerId { get; private set; }
    public User CreatedByManager { get; private set; } = default!;

    public Guid? AssignedToDeveloperId { get; private set; }
    public User? AssignedToDeveloper { get; private set; }

    public bool IsOverdue => Status != TaskStatuss.Completed && DateTime.UtcNow > DueDate;

    private TaskItem() { }

    public TaskItem(string title, string description, User createdBy, DateTime duedate)
    {
        if (createdBy.Role != Role.Manager)
            throw new InvalidOperationException("Only Manager can create a task");

        if(string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException("title");

        if(string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException("description");

        if(duedate <= DateTime.UtcNow)
            throw new ArgumentException("Due date must be in the future", nameof(duedate));

        Title = title;
        Description = description;
        CreatedByManager = createdBy;
        CreatedByManagerId = createdBy.Id;
        Status = TaskStatuss.UnAssigned;
        DueDate = duedate;
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

    public void ChangeDueDate(DateTime newDueDate, User requestBy)
    {
        if(requestBy.Role != Role.Manager)
            throw new InvalidOperationException("Only manager can change due date");
        if(newDueDate <= DateTime.UtcNow)
            throw new ArgumentException("Due date must be in the future", nameof(newDueDate));
        if(Status == TaskStatuss.Completed)
            throw new InvalidOperationException("Cannot change due date of completed task");
        DueDate = newDueDate;
        UpdateTimeStamp();
    }
}
