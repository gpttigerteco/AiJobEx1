namespace AiJobEx1.Shared;

public class Result
{
    public bool Succeeded { get; }
    public string? Error { get; }

    protected Result(bool succeeded, string? error)
    {
        Succeeded = succeeded;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool succeeded, string? error, T? value)
        : base(succeeded, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, null, value);
    public static new Result<T> Failure(string error) => new(false, error, default);
}
