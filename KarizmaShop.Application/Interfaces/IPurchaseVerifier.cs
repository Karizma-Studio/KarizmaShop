using KarizmaPlatform.Shop.Application.Models;
using KarizmaPlatform.Shop.Domain.Interfaces;

namespace KarizmaPlatform.Shop.Application.Interfaces;

/// <summary>
/// Interface for verifying a purchase through a specific market (Myket, CafeBazaar, Test, etc.).
/// With Approach B, ephemeral data (token, etc.) is passed to the verifier's constructor.
/// </summary>
/// <typeparam name="TReward">The reward type associated with the packages for this market.</typeparam>
public interface IPurchaseVerifier<TReward>
{
    /// <summary>
    /// The identifier/name of the market this verifier handles 
    /// (e.g. "CafeBazaar", "Myket", "Test").
    /// </summary>
    string MarketName { get; }

    /// <summary>
    /// Verifies a purchase for a given shop package.
    /// </summary>
    /// <param name="shopPackage">
    /// The shop package being purchased (containing SKU, market, reward, etc.).
    /// </param>
    /// <returns>A result indicating success/failure, plus the typed reward if verified.</returns>
    Task<PurchaseVerificationResult<TReward>> VerifyPurchaseAsync(IShopPackage<TReward> shopPackage);
}