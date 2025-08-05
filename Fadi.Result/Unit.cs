namespace Fadi.Result;

public readonly struct Unit : IEquatable<Unit>
{
	/// <summary>The single Unit value (analogous to `void`).</summary>
	public static Unit Value { get; } = default;
	public bool Equals(Unit other) => true;
	public override bool Equals(object? obj) => obj is Unit;
	public override int GetHashCode() => 0;
	public override string ToString() => "()";

	public static bool operator ==(Unit left, Unit right)
	{
		return left.Equals(right);
	}

	public static bool operator !=(Unit left, Unit right)
	{
		return !(left == right);
	}
}