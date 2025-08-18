using Fadi.Result.Errors;
using Fadi.Result.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Fadi.Result.Tests;

public static class ResultSerializations
{
	static JsonSerializerOptions _jsonOptions = new()
	{
		TypeInfoResolver = new DefaultJsonTypeInfoResolver
		{
			Modifiers =
			{
				new DefaultResultErrorPolymorphicResolver().ResolveDerivedType,
			}
		},
		IncludeFields = true,
	};

	record ModelTest(string Message);

	public class IsSuccess
	{
		[Test]
		public void ReturnsTrueForSuccessfulResult()
		{
			var successful = Result.FromSuccess("Dummy message");
			byte[] serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(successful));

			var response = JsonSerializer.Deserialize<Result>(serializedData);

			Assert.That(response.IsSuccess, Is.True);
		}

		[Test]
		public void ReturnsTrueForSuccessfulGenericResult()
		{
			var successful = Result<ModelTest>.FromSuccess(new ModelTest("Dummy value"));
			byte[] serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(successful));

			var response = JsonSerializer.Deserialize<Result<ModelTest>>(serializedData);

			Assert.That(response.IsSuccess, Is.True);
		}

		[Test]
		public void ReturnsFalseForUnsuccessfulResult()
		{
			var unsuccessful = Result.FromError(new NotFoundError("Dummy message"));
			byte[] serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(unsuccessful, _jsonOptions));

			var response = JsonSerializer.Deserialize<Result<ModelTest>>(serializedData, _jsonOptions);

			Assert.That(response.IsSuccess, Is.False);
		}

		[Test]
		public void ReturnsFalseForUnsuccessfulGenericResult()
		{
			var unsuccessful = Result<ModelTest>.FromError(new NotFoundError("Dummy message"));
			byte[] serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(unsuccessful, _jsonOptions));

			var response = JsonSerializer.Deserialize<Result<ModelTest>>(serializedData, _jsonOptions);

			Assert.That(response.IsSuccess, Is.False);
		}
	}

	public class SuccessMessage
	{
		[Test]
		public void NotEmptyOnDeserializingResult()
		{
			var successful = Result.FromSuccess("Dummy message");
			byte[] serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(successful));

			var response = JsonSerializer.Deserialize<Result>(serializedData);

			Assert.That(response.SuccessMessage, Is.EqualTo("Dummy message"));
		}

		[Test]
		public void NotEmptyOnDeserializingEntityResult()
		{
			var successful = Result.FromSuccess("Dummy message");
			byte[] serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(successful));

			var response = JsonSerializer.Deserialize<Result<string>>(serializedData);
			Assert.That(response.SuccessMessage, Is.EqualTo("Dummy message"));
		}

		[Test]
		public void EmptyOnDeserializingEntityResultUsingFromSuccess()
		{
			var successful = Result<Guid>.FromSuccess(Guid.NewGuid());
			byte[] serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(successful));

			var response = JsonSerializer.Deserialize<Result<string>>(serializedData);
			Assert.That(response.SuccessMessage, Is.Null);
		}

		[Test]
		public void NotEmptyOnDeserializingEntityResultUsingFromSuccessWithMessage()
		{
			var successful = Result<Guid>.FromSuccessWithMessage(Guid.NewGuid(), "Dummy message");
			byte[] serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(successful));

			var response = JsonSerializer.Deserialize<Result<string>>(serializedData);
			Assert.That(response.SuccessMessage, Is.EqualTo("Dummy message"));
		}
	}

	public class Entity
	{
		[Test]
		public void ReturnObjectOnDeserializing()
		{
			var successful = Result<ModelTest>.FromSuccess(new ModelTest("Dummy value"));
			byte[] serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(successful, _jsonOptions));

			var response = JsonSerializer.Deserialize<Result<ModelTest>>(serializedData);

			Assert.That(response.Entity, Is.Not.Null);
		}
	}

	public class Error
	{
		[Test]
		public void ReturnErrorObjectOnDeserializing()
		{
			var unsuccessful = Result<ModelTest>.FromError(new NotFoundError("Dummy message"));
			byte[] serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(unsuccessful, _jsonOptions));

			var response = JsonSerializer.Deserialize<Result<ModelTest>>(serializedData, _jsonOptions);

			Assert.That(response.Error, Is.Not.Null);
		}

		[Test]
		public void ReturnComplexErrorObjectOnDeserializing()
		{
			var validationError = new ValidationError { Message = "Dummy message", Identifier = "Test", Severity = ValidationSeverity.Error };
			var errorResult = new ValidationErrorResult([validationError]);
			var unsuccessful = Result<ModelTest>.FromError(errorResult);
			var serializedResult = JsonSerializer.Serialize(unsuccessful, _jsonOptions);

			var response = JsonSerializer.Deserialize<Result<ModelTest>>(serializedResult, _jsonOptions);

			Assert.That(response.Error, Is.InstanceOf<ValidationErrorResult>());

			var deserializedErrorResult = response.Error as ValidationErrorResult;

			Assert.That(deserializedErrorResult, Is.InstanceOf<ValidationErrorResult>());
			Assert.That(deserializedErrorResult?.ValidationErrors, Has.Length.EqualTo(1));
		}

		[Test]
		public void ReturnComplexErrorObjectOnDeserializing3()
		{
			var error = new NotFoundError("Dummy message");
			var unsuccessful = Result<ModelTest>.FromError(error);

			var serializedResult = JsonSerializer.Serialize(unsuccessful, _jsonOptions);

			var response = JsonSerializer.Deserialize<Result<ModelTest>>(serializedResult, _jsonOptions);

			Assert.That(response.Error, Is.Not.Null);
			Assert.That(response.Error, Is.InstanceOf<NotFoundError>());
		}

		[Test]
		public void ReturnSpecificErrorObjectOnDeserializing()
		{
			var unsuccessful = Result<ModelTest>.FromError(new NotFoundError("Dummy message"));
			string error = JsonSerializer.Serialize(unsuccessful, _jsonOptions);

			Result<ModelTest> response = JsonSerializer.Deserialize<Result<ModelTest>>(error, _jsonOptions);

			Assert.That(response.Error, Is.TypeOf<NotFoundError>());
		}

		[Test]
		public void ReturnExceptionErrorObjectOnDeserializing()
		{
			var unsuccessful = Result<ModelTest>.FromError(new ExceptionError("Dummy message"));
			string error = JsonSerializer.Serialize(unsuccessful, _jsonOptions);

			Result<ModelTest> response = JsonSerializer.Deserialize<Result<ModelTest>>(error, _jsonOptions);

			Assert.That(response.Error, Is.TypeOf<ExceptionError>());
		}
	}
}


