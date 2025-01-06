using Fadi.Result.Errors;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Fadi.Result.Serialization;

public class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
{
	public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
	{
		JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

		Type basePointType = typeof(IResultError);
		if (jsonTypeInfo.Type == basePointType)
		{
			jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
			{
				TypeDiscriminatorPropertyName = "$type",
				IgnoreUnrecognizedTypeDiscriminators = true,
				UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor,
				DerivedTypes =
				{
					new JsonDerivedType(typeof(ResultError), nameof(ResultError)),
					new JsonDerivedType(typeof(ExceptionError), nameof(ExceptionError)),
					new JsonDerivedType(typeof(GenericError), nameof(GenericError)),
					new JsonDerivedType(typeof(NotFoundError), nameof(NotFoundError)),
					new JsonDerivedType(typeof(ValidationErrorResult), nameof(ValidationErrorResult)),
					new JsonDerivedType(typeof(UnauthentectedError), nameof(UnauthentectedError)),
					new JsonDerivedType(typeof(UnauthorizedError), nameof(UnauthorizedError)),
				}
			};
		}

		return jsonTypeInfo;
	}
}