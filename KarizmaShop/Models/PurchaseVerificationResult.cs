namespace KarizmaPlatform.Shop.Models;

/// <summary>
/// Encapsulates the result of a purchase verification attempt.
/// </summary>
/// <typeparam name="TReward">Type of the reward returned on successful verification.</typeparam>
public sealed class PurchaseVerificationResult<TReward>
{
    public bool IsVerified { get; }
    public string? ErrorMessage { get; }
    public int ErrorCode { get; }
    public TReward? Reward { get; }

    private PurchaseVerificationResult(
        bool isVerified, 
        string? errorMessage, 
        int errorCode, 
        TReward? reward)
    {
        IsVerified = isVerified;
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
        Reward = reward;
    }

    /// <summary>
    /// Creates a success result, returning the verified reward.
    /// </summary>
    public static PurchaseVerificationResult<TReward> Success(TReward reward)
        => new PurchaseVerificationResult<TReward>(true, null, 0, reward);

    /// <summary>
    /// Creates a fail result, returning an error message and code.
    /// </summary>
    public static PurchaseVerificationResult<TReward> Fail(string errorMessage, int errorCode)
        => new PurchaseVerificationResult<TReward>(false, errorMessage, errorCode, default);
}