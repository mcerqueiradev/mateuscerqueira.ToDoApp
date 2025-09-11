namespace mateuscerqueira.ToDoApp.Domain.Core;

public class ApiResponse<T>
{
    public required bool Successed { get; init; }
    public string? Message { get; set; }
    public T? Data { get; init; }
}
