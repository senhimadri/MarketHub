namespace MarketHub.Common.Library.OperationResult;

public sealed record Error(int Code, string? Description = null)
{
    public static readonly Error None = new(200, string.Empty);
}

public class Errors
{
    public static readonly Error ContentNotFound = new Error(404, "Content not found.");
    public static readonly Error BadRequest = new Error(400, "Bad request.");
    public static readonly Error InternalServerError = new Error(500, "Internal server error.");
    public static readonly Error UnauthorizedAccess = new Error(401, "Unauthorized access.");
    public static readonly Error Forbidden = new Error(403, "Forbidden access.");
    public static readonly Error ValidationFailed = new Error(422, "Validation failed.");
    public static Error NewError(int code, string message) => new Error(code, message);
}
