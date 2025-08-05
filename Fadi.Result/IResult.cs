namespace Fadi.Result;

public interface IResult<T>
{
	bool IsSuccess { get; }
	bool IsFailed { get; }
	bool IsDefined { get; }

	/// <summary>
	/// Optional success message
	/// </summary>
	string? SuccessMessage { get; }

	/// <summary>
	/// Polymorphic error
	/// </summary>
	IResultError? Error { get; }

	/// <summary>
	/// the value (Unit if none)
	/// </summary>
	T Entity { get; }
}