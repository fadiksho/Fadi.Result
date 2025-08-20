using Fadi.Result.Errors;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Fadi.Result.Serialization;

public class DefaultResultErrorPolymorphicResolver : IResultErrorPolymorphicResolver
{
	public static Type BaseType { get; } = IResultErrorPolymorphicResolver.BaseType;

	public JsonDerivedType[] DerivedTypes =>
	[
		new(typeof(ExceptionError), nameof(ExceptionError)),
		new(typeof(GenericError), nameof(GenericError)),
		new(typeof(NotFoundError), nameof(NotFoundError)),
		new(typeof(ValidationErrorResult), nameof(ValidationErrorResult)),
		new(typeof(UnauthenticatedError), nameof(UnauthenticatedError)),
		new(typeof(UnauthorizedError), nameof(UnauthorizedError)),
		new(typeof(ResultError), nameof(ResultError)),
	];

	public void ResolveDerivedType(JsonTypeInfo jsonTypeInfo)
	{
		if (jsonTypeInfo.Type == IResultErrorPolymorphicResolver.BaseType)
		{
			jsonTypeInfo.PolymorphismOptions ??= new JsonPolymorphismOptions();
			jsonTypeInfo.PolymorphismOptions.UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor;

			foreach (var derived in DerivedTypes)
			{
				if (jsonTypeInfo.PolymorphismOptions.DerivedTypes.IndexOf(derived) < 0)
				{
					jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add(derived);
				}
			}
		}
	}
}