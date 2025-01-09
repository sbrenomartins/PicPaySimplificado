namespace PicPaySimplificado.Models.DTOs.Responses;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public string ErrorMessage { get; private set; }
    public T Value { get; private set; }

    private Result(bool isSuccess, string errorMessage, T value)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Value = value;
    }

    private Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    public static Result<T> Success(T value) => new Result<T>(true, null, value);
    public static Result<T> Failure(string errorMessage) => new Result<T>(false, errorMessage, default);
}