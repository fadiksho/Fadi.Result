namespace Fadi.Result;

public record ResultError : IResultError
{
  public ResultError(string message)
  {
    Message = message;
  }
  public string Message { get; }
}
