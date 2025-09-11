namespace mateuscerqueira.Application.Common.Responses;

public sealed record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NotImplemented = new("NotImplemented", "Method not implemented");
    public static readonly Error EmptyDatabase = new("EmpytDatabase", "Not data found");
    public static readonly Error ExistingUser = new("ExistingUser", "There is a user with this email");
    public static readonly Error InvalidInput = new("InvalidInput", "The input provided is invalid");
    public static readonly Error UnauthorizedAccess = new("UnauthorizedAccess", "User is not authorized to perform this action");
    public static readonly Error Timeout = new("Timeout", "The operation timed out");
    public static readonly Error ServiceUnavailable = new("ServiceUnavailable", "The service is currently unavailable");
    public static readonly Error DatabaseError = new("DatabaseError", "An error occurred while accessing the database");
    public static readonly Error InvalidCredentials = new("InvalidCredentials", "The provided credentials are invalid");
    public static readonly Error UserLockedOut = new("UserLockedOut", "The user account is locked out");
    public static readonly Error PasswordExpired = new("PasswordExpired", "The user password has expired");
    public static readonly Error ConcurrencyConflict = new("ConcurrencyConflict", "A concurrency conflict occurred");
    public static readonly Error ResourceConflict = new("ResourceConflict", "A resource conflict occurred");
    public static readonly Error InsufficientPermissions = new("InsufficientPermissions", "Insufficient permissions to perform this action");
    public static readonly Error InvalidToken = new("InvalidToken", "The provided token is invalid or expired");
    public static readonly Error NotFound = new("NotFound", "The requested resource was not found");
    public static readonly Error ExternalServiceError = new("ExternalServiceError", "An error occurred in an external service");
    public static readonly Error BadRequest = new("BadRequest", "The request was invalid or cannot be served");
    public static readonly Error Conflict = new("Conflict", "The request could not be completed due to a conflict");
    public static readonly Error UserNotFound = new("UserNotFound", "The request could not find your credentials");
}
