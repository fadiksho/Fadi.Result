using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Fadi.Result;

public readonly record struct Result<TEntity> : IResult<TEntity>
{
	public TEntity Entity { get; }
	public string? SuccessMessage { get; }
	public IResultError? Error { get; }

	// A result is defined if it has either an error or a non-default entity.
	[JsonIgnore]
	public bool IsDefined => IsSuccess || IsFailed;

	[MemberNotNullWhen(false, nameof(Error))]
	public bool IsSuccess { get; }

	[MemberNotNullWhen(true, nameof(Error))]
	public bool IsFailed => Error != null;

	[JsonConstructor]
	public Result(TEntity? entity, IResultError? error, bool isSuccess, string? successMessage)
	{
		Entity = entity!;
		Error = error;
		IsSuccess = isSuccess;
		SuccessMessage = successMessage;
	}

	// Static factory for a successful result (no message).
	public static Result<TEntity> FromSuccess(TEntity entity)
			=> new(entity, null, true, null);

	// Static factory for success with a message.
	public static Result<TEntity> FromSuccess(TEntity entity, string successMessage)
			=> new(entity, null, true, successMessage);

	// Static factory for an error result.
	public static Result<TEntity> FromError<TError>(TError error) where TError : IResultError
			=> new(default, error, false, null);

	// Implicit conversion from the value type to a success result.
	public static implicit operator Result<TEntity>(TEntity entity)
			=> new(entity, null, true, null);

	// Implicit conversion from a ResultError to an error result.
	public static implicit operator Result<TEntity>(ResultError error)
			=> new(default, error, false, null);
}

public static class Result
{
	/// <summary>Success with no data (no message).</summary>
	public static Result<Unit> FromSuccess()
			=> new(Unit.Value, null, true, null);

	/// <summary>Success with no data and a message.</summary>
	public static Result<Unit> FromSuccess(string successMessage)
			=> new(Unit.Value, null, true, successMessage);

	/// <summary>Error with no data.</summary>
	public static Result<Unit> FromError<TError>(TError error) where TError : IResultError
			=> new(default, error, false, null);
}