using HotelOccupancy.Domain.Models.Errors;

namespace HotelOccupancy.Application.Common;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;
    public string? ErrorCode { get; private set; }
    public string? ErrorMessage { get; private set; }
    public T? Data { get; private set; }

    private Result() { }

    public static Result<T> Success(T data)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data
        };
    }

    public static Result<T> Failure(string errorCode, string errorMessage)
    {
        return new Result<T>
        {
            IsSuccess = false,
            ErrorCode = errorCode,
            ErrorMessage = errorMessage
        };
    }
    
    public static Result<T> NotFound(string? message = null)
        => Failure(ErrorCodes.NotFound, message ?? ErrorMessages.NotFound);

    public static Result<T> InvalidRequest(string? message = null)
        => Failure(ErrorCodes.InvalidRequest, message ?? ErrorMessages.InvalidRequest);
}