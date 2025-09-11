namespace mateuscerqueira.ToDoApp.Domain._Abstractions;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }  
    string CreatedBy { get; set; } 
    DateTime UpdatedAt { get; set; }
    string UpdatedBy { get; set; }

    void TrackCreation(string createdBy);
    void TrackUpdate(string updatedBy);
}