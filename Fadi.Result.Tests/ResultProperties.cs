namespace Fadi.Result.Tests;
public class ResultProperties
{
	public record DummyEntity { }

	[TestCase]
	public void WhenCreatedFromError()
	{
		Result<Unit> result = Result.FromError(new ResultError("error"));

		Assert.Multiple(() =>
		{
			Assert.That(result.IsSuccess, Is.False);
			Assert.That(result.Entity, Is.InstanceOf<Unit>());
			Assert.That(result.IsDefined, Is.True);
			Assert.That(result.SuccessMessage, Is.Null);
			Assert.That(result.Error, Is.Not.Null);
			Assert.That(result.Error, Is.InstanceOf<ResultError>());
		});
	}

	[TestCase]
	public void WhenCreatedFromErrorGeneric()
	{
		Result<DummyEntity> result = Result<DummyEntity>.FromError(new ResultError("error"));

		Assert.Multiple(() =>
		{
			Assert.That(result.IsSuccess, Is.False);
			Assert.That(result.Entity, Is.Null);
			Assert.That(result.IsDefined, Is.True);
			Assert.That(result.SuccessMessage, Is.Null);
			Assert.That(result.Error, Is.Not.Null);
			Assert.That(result.Error, Is.InstanceOf<ResultError>());
		});
	}

	[TestCase]
	public void WhenCreatedFromSuccessGeneric()
	{
		Result<DummyEntity> result = Result<DummyEntity>.FromSuccess(new DummyEntity());

		Assert.Multiple(() =>
		{
			Assert.That(result.IsSuccess, Is.True);
			Assert.That(result.IsFailed, Is.False);
			Assert.That(result.IsDefined, Is.True);
			Assert.That(result.Entity, Is.Not.Null);
			Assert.That(result.Entity, Is.InstanceOf<DummyEntity>());
			Assert.That(result.SuccessMessage, Is.Null);
		});
	}

	[TestCase]
	public void WhenCreatedFromSuccessWithMessageGeneric()
	{
		Result<DummyEntity> result =
			Result<DummyEntity>.FromSuccess(new DummyEntity(), "success");

		Assert.Multiple(() =>
		{
			Assert.That(result.IsSuccess, Is.True);
			Assert.That(result.IsFailed, Is.False);
			Assert.That(result.IsDefined, Is.True);
			Assert.That(result.Entity, Is.Not.Null);
			Assert.That(result.Entity, Is.InstanceOf<DummyEntity>());
			Assert.That(result.SuccessMessage, Is.Not.Null);
		});
	}

	[TestCase]
	public void WhenDefined()
	{
		Result<Unit> result = default;
		Assert.Multiple(() =>
		{
			Assert.That(result.IsSuccess, Is.False);
			Assert.That(result.IsFailed, Is.False);
			Assert.That(result.IsDefined, Is.False);
			Assert.That(result.Error, Is.Null);
			Assert.That(result.SuccessMessage, Is.Null);
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