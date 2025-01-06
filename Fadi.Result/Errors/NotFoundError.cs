namespace Fadi.Result.Errors;

public sealed record NotFoundError(string Message = "The searched-for entity was not found.") : ResultError(Message);