using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Fadi.Result.Errors;

public record ExceptionError : ResultError
{
  public string TraceId { get; }
  public string SpanId { get; }
  public DateTimeOffset TimeStamp { get; }

  public ExceptionError(string message)
      : base(message)
  {
    TraceId = Activity.Current?.TraceId.ToString() ?? "";
    SpanId = Activity.Current?.SpanId.ToString() ?? "";
    TimeStamp = DateTimeOffset.UtcNow;
  }

  [JsonConstructor]
  public ExceptionError(string message, string traceId, string spanId, DateTimeOffset timeStamp)
      : base(message)
  {
    TraceId = traceId;
    SpanId = spanId;
    TimeStamp = timeStamp;
  }
}
