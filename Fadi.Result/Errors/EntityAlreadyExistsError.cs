namespace Fadi.Result.Errors;
public sealed record EntityAlreadyExistsError(string Message = "The entity already exists.") : ResultError(Message);