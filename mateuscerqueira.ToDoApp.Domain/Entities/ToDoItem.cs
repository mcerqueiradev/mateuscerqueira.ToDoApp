using mateuscerqueira.ToDoApp.Domain._Abstractions;
using mateuscerqueira.ToDoApp.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace mateuscerqueira.ToDoApp.Domain.Entities;

public class ToDoItem : AuditableEntity, IEntity<Guid>
{
    [Required]
    public Guid Id{ get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; private set; }

    [MaxLength(1000)]
    public string? Description { get; private set; }

    public bool IsCompleted { get; private set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Relacionamento opcional
    public Guid? UserId { get; private set; }
    public virtual User? User { get; private set; }

    private ToDoItem() : base() { }

    public ToDoItem(string title, string? description = null, Guid? userId = null)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        UserId = userId;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateTitle(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
            throw new ArgumentException("Title cannot be empty");

        Title = newTitle;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string? newDescription)
    {
        Description = newDescription;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsIncomplete()
    {
        IsCompleted = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignToUser(Guid userId)
    {
        UserId = userId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UnassignFromUser()
    {
        UserId = null;
        UpdatedAt = DateTime.UtcNow;
    }

}