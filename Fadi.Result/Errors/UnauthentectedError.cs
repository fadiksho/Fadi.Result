namespace Fadi.Result.Errors;

public sealed record UnauthentectedError(string Message = "401") : ResultError(Message);