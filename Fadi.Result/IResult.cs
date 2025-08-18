using System.Diagnostics.CodeAnalysis;

namespace Fadi.Result;

public interface IResult
{
	bool IsSuccess { get; }
	bool IsFailed { get; }
	bool IsDefined { get; }
	string SuccessMessage { get; }
	IResultError? Error { get; }
}

public interface IResult<out TEntity> : IResult
{
	[AllowNull]
	TEntity Entity { get; }
}