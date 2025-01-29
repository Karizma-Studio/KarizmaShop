using KarizmaPlatform.Shop.Application.Interfaces;
using KarizmaPlatform.Shop.Application.Models;
using KarizmaPlatform.Shop.Domain.Interfaces;

namespace KarizmaPlatform.Shop.Application.Verifiers;

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