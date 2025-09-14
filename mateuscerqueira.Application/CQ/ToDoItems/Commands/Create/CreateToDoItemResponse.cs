﻿namespace mateuscerqueira.Application.CQ.ToDoItems.Commands.Create
{
    public class CreateToDoItemResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public Guid? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
