namespace MarketHub.Common.Library.OperationResult;

public class OperationResult
{
    protected OperationResult(bool isSuccess, Error error, Dictionary<string, string[]>? validationErrors = null)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            throw new ArgumentException("Invalid error.");

        IsSuccess = isSuccess;
        Error = error;

        ValidationErrors = validationErrors;
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    public readonly Dictionary<string, string[]>? ValidationErrors;
    public bool IsValidationFailure => ValidationErrors?.Any() == true;
    public static OperationResult Success() => new(true, Error.None);
    private static OperationResult Failure(Error error) => new(false, error);
    public static OperationResult ValidationFailure(Dictionary<string, string[]> errors) => new(false, Errors.ValidationFailed, errors);
    public static implicit operator OperationResult(Error error) => Failure(error);
}

