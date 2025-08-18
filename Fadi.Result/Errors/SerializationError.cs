namespace Fadi.Result.Errors;

public sealed record SerializationError(string Message = "Serialization error.") : ResultError(Message);