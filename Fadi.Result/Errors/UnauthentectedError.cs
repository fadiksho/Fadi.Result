namespace Fadi.Result.Errors;

public sealed record UnauthenticatedError(string Message = "401") : ResultError(Message);