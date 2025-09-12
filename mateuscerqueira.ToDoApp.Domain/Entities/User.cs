using mateuscerqueira.ToDoApp.Domain._Abstractions;
using mateuscerqueira.ToDoApp.Domain.Common;
using mateuscerqueira.ToDoApp.Domain.Enums;
using mateuscerqueira.ToDoApp.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;


namespace mateuscerqueira.ToDoApp.Domain.Entities;

public class User : AuditableEntity, IEntity<Guid>
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public PersonalName Name { get; private set; }

    [Required]
    public Email Email { get; private set; }

    [Required]
    public UserRole Role { get; private set; }

    [Required]
    public PasswordHash Password { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime? LastLoginDate { get; private set; }

    public virtual ICollection<ToDoItem> ToDoItems { get; private set; } = new List<ToDoItem>();

    protected User() { }

    public User(
        PersonalName name,
        Email email,
        PasswordHash password,
        UserRole role = UserRole.ROLE_MEMBER)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        IsActive = true;
        LastLoginDate = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdatePersonalInfo(PersonalName newName)
    {
        Name = newName;
    }

    public void ChangePassword(PasswordHash newPassword)
    {
        Password = newPassword;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void UpdateLastLogin()
    {
        LastLoginDate = DateTime.UtcNow;
    }

    public void ChangeRole(UserRole newRole)
    {
        Role = newRole;
    }
}