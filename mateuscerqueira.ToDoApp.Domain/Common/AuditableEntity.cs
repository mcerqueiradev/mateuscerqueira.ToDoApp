using mateuscerqueira.ToDoApp.Domain._Abstractions;

namespace mateuscerqueira.ToDoApp.Domain.Common;

public abstract class AuditableEntity : IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }

    protected AuditableEntity() : base()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // Métodos para atualizar informações.
    public void SetCreated(string createdBy, DateTime? createdAt = null)
    {
        CreatedBy = createdBy ?? throw new ArgumentNullException(nameof(createdBy));
        CreatedAt = createdAt ?? DateTime.UtcNow;
        UpdatedAt = CreatedAt;
        UpdatedBy = CreatedBy;
    }

    public void SetUpdated(string updatedBy, DateTime? updatedAt = null)
    {
        UpdatedBy = updatedBy ?? throw new ArgumentNullException(nameof(updatedBy));
        UpdatedAt = updatedAt ?? DateTime.UtcNow;
    }

    // Método para rastrear criação.
    public void TrackCreation(string createdBy)
    {
        if (CreatedAt == default)
        {
            SetCreated(createdBy);
        }
    }

    // Método para rastrear atualização.
    public void TrackUpdate(string updatedBy)
    {
        SetUpdated(updatedBy);
    }
}
