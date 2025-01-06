using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Fadi.Result;

public readonly record struct Result : IResult
{
	[MemberNotNullWhen(false, nameof(Error))]
	public readonly bool IsSuccess => Error is null && IsDefined;

	public readonly bool IsFailed => !IsSuccess && IsDefined;

	[JsonIgnore]
	public readonly bool IsDefined
	{
		get
		{
			if (Error is not null)
				return true;

			if (!string.IsNullOrEmpty(SuccessMessage))
				return true;

			return false;
		}
	}

	[MemberNotNullWhen(true, nameof(IsSuccess))]
	public string SuccessMessage { get; }

	public IResultError? Error { get; }

	[JsonConstructor]
	public Result(string successMessage, IResultError? error)
	{
		Error = error;
		SuccessMessage = successMessage;
	}

	public static Result FromSuccess(string successMessage)
			=> new(successMessage, default);

	public static Result FromError<TError>(TError error) where TError : IResultError
			=> new(default!, error);

	public static implicit operator Result(ResultError error)
			=> new(default!, error);
}

public readonly record struct Result<TEntity> : IResult<TEntity>
{
	[AllowNull]
	public TEntity Entity { get; }

	[MemberNotNullWhen(false, nameof(Error))]
	[MemberNotNullWhen(true, nameof(Entity))]
	public readonly bool IsSuccess => Error is null && IsDefined;

	[MemberNotNullWhen(true, nameof(Error))]
	public readonly bool IsFailed => !IsSuccess && IsDefined;

	[JsonIgnore]
	public readonly bool IsDefined
	{
		get
		{
			if (Error is not null)
				return true;

			if (Entity is not null)
				return true;

			return false;
		}
	}

	[AllowNull]
	public string SuccessMessage { get; }

	public IResultError? Error { get; }

	[JsonConstructor]
	public Result(TEntity? entity, IResultError? error, string? successMessage)
	{
		Error = error;
		SuccessMessage = successMessage;
		Entity = entity;
	}

	public static Result<TEntity> FromSuccess(TEntity entity) => new(entity, default, default);

	public static Result<TEntity> FromSuccessWithMessage(TEntity entity, string successMessage) => new(entity, default, successMessage);

	public static Result<TEntity> FromError<TError>(TError error) where TError : IResultError
			=> new(default, error, default);

	public static implicit operator Result<TEntity>(TEntity? entity)
	{
		return new(entity, default, default);
	}

	public static implicit operator Result<TEntity>(ResultError error)
	{
		return new(default, error, default);
	}
}
