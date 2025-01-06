using System.Text.Json.Serialization.Metadata;

namespace Fadi.Result.Serialization;

public interface IResultErrorPolymorphicResolver
{
	public static Type BaseType { get; } = typeof(IResultError);

	public static JsonDerivedType[] DerivedTypes { get; } = [];

	public void ResolveDerivedType(JsonTypeInfo jsonTypeInfo);
}