namespace Common;

public class ResultWrapper<T>
{
	public T? Value { get; set; }
	public string? ErrorCode { get; set; }
	public bool IsSuccess { get; set; }

	public static ResultWrapper<T> Success(T value) =>
		new() { Value = value, IsSuccess = true };
	public static ResultWrapper<T> Failure(ErrorCode errorCode) =>
		new() { ErrorCode = errorCode.ToString(), IsSuccess = false };
}
