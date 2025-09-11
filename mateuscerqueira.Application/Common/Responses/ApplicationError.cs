
namespace mateuscerqueira.Application.Common.Responses;

public static class ApplicationError
{
    public static readonly Error NotFound = new("NotFound", "Entity not found");
    public static readonly Error EmptyDatabase = new("EmpytDatabase", "Not data found");
}
