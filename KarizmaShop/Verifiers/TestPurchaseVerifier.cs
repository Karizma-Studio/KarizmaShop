using KarizmaPlatform.Shop.Interfaces;
using KarizmaPlatform.Shop.Models;

namespace KarizmaPlatform.Shop.Verifiers;

/// <summary>
/// A Test purchase verifier that always succeeds. 
/// Useful for local testing or development.
/// </summary>
public class TestPurchaseVerifier<TReward> : IPurchaseVerifier<TReward>
{
    public string MarketName => "Test";

    public Task<PurchaseVerificationResult<TReward>> VerifyPurchaseAsync(IShopPackage<TReward> shopPackage)
    {
        // Always returns success, passing the package's reward.
        return Task.FromResult(
            PurchaseVerificationResult<TReward>.Success(shopPackage.GetRewards())
        );
    }
}