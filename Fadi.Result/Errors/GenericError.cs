namespace Fadi.Result.Errors;

public sealed record GenericError(string Message) : ResultError(Message);