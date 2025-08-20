namespace Fadi.Result.Tests;
public class ResultProperties
{
	public record DummyEntity { }

	public class NonGeneric
	{
		[TestCase]
		public void WhenCreatedFromError()
		{
			Result result = Result.FromError(new ResultError("error"));

			Assert.Multiple(() =>
			{
				Assert.That(result.IsSuccess, Is.False);
				Assert.That(result.IsDefined, Is.True);
				Assert.That(result.SuccessMessage, Is.Null);
				Assert.That(result.Error, Is.Not.Null);
				Assert.That(result.Error, Is.InstanceOf<ResultError>());
				Assert.That(result.Error?.Message, Is.EqualTo("error"));
			});
		}

		[TestCase]
		public void WhenCreatedFromSuccess()
		{
			Result result = Result.FromSuccess();

			Assert.Multiple(() =>
			{
				Assert.That(result.IsSuccess, Is.True);
				Assert.That(result.IsFailed, Is.False);
				Assert.That(result.IsDefined, Is.True);
				Assert.That(result.Error, Is.Null);
				Assert.That(result.SuccessMessage, Is.Null);
			});
		}

		[TestCase]
		public void WhenCreatedFromSuccessWithSuccessMessage()
		{
			Result result = Result.FromSuccess("success");

			Assert.Multiple(() =>
			{
				Assert.That(result.IsSuccess, Is.True);
				Assert.That(result.IsFailed, Is.False);
				Assert.That(result.IsDefined, Is.True);
				Assert.That(result.Error, Is.Null);
				Assert.That(result.SuccessMessage, Is.EqualTo("success"));
			});
		}

		[TestCase]
		public void WhenDefined()
		{
			Result result = new();

			Assert.Multiple(() =>
			{
				Assert.That(result.IsSuccess, Is.False);
				Assert.That(result.IsFailed, Is.False);
				Assert.That(result.IsDefined, Is.False);
				Assert.That(result.Error, Is.Null);
				Assert.That(result.SuccessMessage, Is.Null);
			});
		}
	}

	public class Generic
	{
		[TestCase]
		public void WhenCreatedFromError()
		{
			Result<DummyEntity> result = Result<DummyEntity>.FromError(new ResultError("error"));

			Assert.Multiple(() =>
			{
				Assert.That(result.IsSuccess, Is.False);
				Assert.That(result.IsFailed, Is.True);
				Assert.That(result.Entity, Is.Null);
				Assert.That(result.IsDefined, Is.True);
				Assert.That(result.SuccessMessage, Is.Null);
				Assert.That(result.Error, Is.Not.Null);
				Assert.That(result.Error, Is.InstanceOf<ResultError>());
				Assert.That(result.Error?.Message, Is.EqualTo("error"));
			});
		}

		[TestCase]
		public void WhenCreatedFromSuccess()
		{
			Result<DummyEntity> result = Result<DummyEntity>.FromSuccess(new DummyEntity());

			Assert.Multiple(() =>
			{
				Assert.That(result.IsSuccess, Is.True);
				Assert.That(result.IsFailed, Is.False);
				Assert.That(result.IsDefined, Is.True);
				Assert.That(result.Entity, Is.Not.Null);
				Assert.That(result.Entity, Is.InstanceOf<DummyEntity>());
				Assert.That(result.Error, Is.Null);
				Assert.That(result.SuccessMessage, Is.Null);
			});
		}

		[TestCase]
		public void WhenCreatedFromSuccessWithSuccessMessage()
		{
			Result<DummyEntity> result =
				Result<DummyEntity>.FromSuccessWithMessage(new DummyEntity(), "success");

			Assert.Multiple(() =>
			{
				Assert.That(result.IsSuccess, Is.True);
				Assert.That(result.IsFailed, Is.False);
				Assert.That(result.IsDefined, Is.True);
				Assert.That(result.Entity, Is.Not.Null);
				Assert.That(result.Entity, Is.InstanceOf<DummyEntity>());
				Assert.That(result.Error, Is.Null);
				Assert.That(result.SuccessMessage, Is.Not.Null);
			});
		}

		[TestCase]
		public void WhenDefinedGeneric()
		{
			Result<DummyEntity> result = new();

			Assert.Multiple(() =>
			{
				Assert.That(result.IsSuccess, Is.False);
				Assert.That(result.IsFailed, Is.False);
				Assert.That(result.IsDefined, Is.False);
				Assert.That(result.Error, Is.Null);
				Assert.That(result.Entity, Is.Null);
				Assert.That(result.SuccessMessage, Is.Null);
			});
		}
	}
}