namespace Fadi.Result.Errors;

public sealed record UnauthorizedError(string Message = "403") : ResultError(Message);