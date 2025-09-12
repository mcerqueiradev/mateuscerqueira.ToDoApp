namespace mateuscerqueira.Application.Common.Responses;

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NotImplemented = new("NOT_IMPLEMENTED", "Method not implemented");
    public static readonly Error EmptyDatabase = new("EMPTY_DATABASE", "No data found");
    public static readonly Error InvalidInput = new("INVALID_INPUT", "The input provided is invalid");
    public static readonly Error UnauthorizedAccess = new("UNAUTHORIZED_ACCESS", "User is not authorized to perform this action");
    public static readonly Error Timeout = new("TIMEOUT", "The operation timed out");
    public static readonly Error ServiceUnavailable = new("SERVICE_UNAVAILABLE", "The service is currently unavailable");
    public static readonly Error DatabaseError = new("DATABASE_ERROR", "An error occurred while accessing the database");
    public static readonly Error InvalidCredentials = new("INVALID_CREDENTIALS", "The provided credentials are invalid");
    public static readonly Error UserLockedOut = new("USER_LOCKED_OUT", "The user account is locked out");
    public static readonly Error PasswordExpired = new("PASSWORD_EXPIRED", "The user password has expired");
    public static readonly Error ConcurrencyConflict = new("CONCURRENCY_CONFLICT", "A concurrency conflict occurred");
    public static readonly Error ResourceConflict = new("RESOURCE_CONFLICT", "A resource conflict occurred");
    public static readonly Error InsufficientPermissions = new("INSUFFICIENT_PERMISSIONS", "Insufficient permissions to perform this action");
    public static readonly Error InvalidToken = new("INVALID_TOKEN", "The provided token is invalid or expired");
    public static readonly Error NotFound = new("NOT_FOUND", "The requested resource was not found");
    public static readonly Error ExternalServiceError = new("EXTERNAL_SERVICE_ERROR", "An error occurred in an external service");
    public static readonly Error BadRequest = new("BAD_REQUEST", "The request was invalid or cannot be served");
    public static readonly Error Conflict = new("CONFLICT", "The request could not be completed due to a conflict");

    // Users
    public static readonly Error UserNotFound = new("USER_NOT_FOUND", "User not found");
    public static readonly Error InvalidPassword = new("INVALID_PASSWORD", "Current password is invalid");
    public static readonly Error ExistingUser = new("EXISTING_USER", "A user with this email already exists");
    public static readonly Error UserHasAssociatedTasks = new("USER_HAS_TASKS", "Cannot delete user with associated tasks");

    // ToDoItems
    public static readonly Error ToDoItemNotFound = new("TODOITEM_NOT_FOUND", "Task not found");
    public static readonly Error UnauthorizedToDoItemAccess = new("UNAUTHORIZED_TODO_ACCESS", "You are not authorized to access this task");

    // Authentication
    public static readonly Error UserInactive = new("USER_INACTIVE", "User account is deactivated");
    public static readonly Error TokenExpired = new("TOKEN_EXPIRED", "Authentication token has expired");
}