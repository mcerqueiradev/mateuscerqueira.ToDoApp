namespace mateuscerqueira.Application.CQ.ToDoItems.Commands.MarkAsComplete;

public class MarkToDoItemAsCompleteResponse
{
    public Guid Id { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime UpdatedAt { get; set; }
}
