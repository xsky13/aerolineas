namespace Aerolineas.Config;

public class Result<T>
{
    public bool Success { get; init; }
    public T? Value { get; init; }
    public string? Error { get; init; }
    public int StatusCode { get; init; }

    private Result() { }

    public static Result<T> Fail(string message)
    {
        return new() { Error = message, Success = false, StatusCode = 200 };
    }

    public static Result<T> Ok(T data, int statusCode = 400)
    {
        return new() { Success = true, Value = data, StatusCode = statusCode };
    }
}