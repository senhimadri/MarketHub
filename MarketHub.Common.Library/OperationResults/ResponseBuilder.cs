using MarketHub.Common.Library.OperationResult;

namespace MarketHub.Identity.Service;

public static class ResponseBuilder
{
    public static T Match<T>(this OperationResult result,
    Func<T> onSuccess,
    Func<Dictionary<string, string[]>, T> onValidationFailure,
    Func<Error, T> onFailure)
    {
        if (result.IsSuccess)
            return onSuccess();

        if (result.IsValidationFailure && result.ValidationErrors != null)
            return onValidationFailure(result.ValidationErrors);

        return onFailure(result.Error);
    }
}
