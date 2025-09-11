namespace mateuscerqueira.ToDoApp.Domain._Abstractions;

public interface IEntity<Guid>
{
    public Guid Id { get; set; }

}