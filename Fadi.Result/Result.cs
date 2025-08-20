using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Fadi.Result;

public readonly record struct Result : IResult
{
	public bool IsSuccess { get; }

	[MemberNotNullWhen(true, nameof(Error))]
	public bool IsFailed { get; }

	[JsonIgnore]
	public readonly bool IsDefined
	{
		get
		{
			return IsSuccess || IsFailed;
		}
	}

	public string? SuccessMessage { get; }

	public IResultError? Error { get; }

	[JsonConstructor]
	public Result(bool isSuccess, bool isFailed, string? successMessage, IResultError? error)
	{
		IsSuccess = isSuccess;
		IsFailed = isFailed;

		if (isFailed && isSuccess)
			throw new InvalidOperationException("A result cannot be both successful and failed at the same time.");

		Error = error;
		SuccessMessage = successMessage;
	}

	public static Result FromSuccess()
			=> new(true, false, default, default);

	public static Result FromSuccess(string successMessage)
			=> new(true, false, successMessage, default);

	public static Result FromError<TError>(TError error) where TError : IResultError
			=> new(false, true, default, error);

	public static implicit operator Result(ResultError error)
			=> new(false, true, default, error);
}

public readonly record struct Result<TEntity> : IResult<TEntity>
{
	[AllowNull]
	public TEntity Entity { get; }

	[MemberNotNullWhen(true, nameof(Entity))]
	public bool IsSuccess { get; }

	[MemberNotNullWhen(true, nameof(Error))]
	public bool IsFailed { get; }

	[JsonIgnore]
	public readonly bool IsDefined
	{
		get
		{
			return IsSuccess || IsFailed;
		}
	}

	public string? SuccessMessage { get; }

	public IResultError? Error { get; }

	[JsonConstructor]
	public Result(bool isSuccess, bool isFailed, TEntity? entity, IResultError? error, string? successMessage)
	{
		IsSuccess = isSuccess;
		IsFailed = isFailed;
		Error = error;
		SuccessMessage = successMessage;
		Entity = entity;
	}

	public static Result<TEntity> FromSuccess(TEntity entity)
		=> new(true, false, entity, default, default);

	public static Result<TEntity> FromSuccessWithMessage(TEntity entity, string successMessage)
		=> new(true, false, entity, default, successMessage);

	public static Result<TEntity> FromError<TError>(TError error) where TError : IResultError
			=> new(false, true, default, error, default);

	public static implicit operator Result<TEntity>(TEntity? entity)
	{
		return new(true, false, entity, default, default);
	}

	public static implicit operator Result<TEntity>(ResultError error)
	{
		return new(false, true, default, error, default);
	}
}
