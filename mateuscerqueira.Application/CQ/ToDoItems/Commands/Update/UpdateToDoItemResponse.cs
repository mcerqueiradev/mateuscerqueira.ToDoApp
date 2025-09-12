﻿namespace mateuscerqueira.Application.CQ.ToDoItems.Commands.Update;
public class UpdateToDoItemResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public Guid? UserId { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
