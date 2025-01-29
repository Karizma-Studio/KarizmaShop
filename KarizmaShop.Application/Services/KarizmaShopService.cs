using KarizmaPlatform.Shop.Application.Interfaces;
using KarizmaPlatform.Shop.Application.Models;
using KarizmaPlatform.Shop.Domain.Interfaces;
using Microsoft.Extensions.Options;

namespace KarizmaPlatform.Shop.Application.Services;

/// <summary>
/// Example service to store and retrieve shop packages,
/// then help with ephemeral verifiers in Approach B.
/// </summary>
/// <typeparam name="TReward">The typed reward for packages.</typeparam>
public class KarizmaShopService<TReward>
{
    private readonly List<IShopPackage<TReward>> _packages = [];
    private Dictionary<string, IShopPackage<TReward>> _idMap = new();
    
    
    /// <summary>
    /// Set the shop packages to use in this service.
    /// </summary>
    public void SetPackages(IEnumerable<IShopPackage<TReward>> packages)
    {
        _packages.Clear();
        _packages.AddRange(packages);
        _idMap = _packages.ToDictionary(p => p.GetId());
    }


    /// <summary>
    /// Find a package by SKU and market.
    /// Returns null if not found.
    /// </summary>
    public IShopPackage<TReward>? GetPackage(string sku, string market)
    {
        return _packages.FirstOrDefault(p =>
            p.GetSku() == sku &&
            p.GetMarket() == market);
    }

    /// <summary>
    /// Find a package by its unique identifier.
    /// Returns null if not found.
    /// </summary>
    public IShopPackage<TReward>? GetPackage(string id)
    {
        return _idMap.GetValueOrDefault(id);
    }
    
    /// <summary>
    /// Returns all packages in this service.
    /// </summary>
    public IEnumerable<IShopPackage<TReward>> GetPackages()
    {
        return _packages.ToList();
    }

    /// <summary>
    /// Verifies a purchase by creating a new ephemeral verifier object 
    /// (passed in via a factory) and calling its verification method.
    /// </summary>
    /// <param name="sku">SKU of the package being purchased.</param>
    /// <param name="market">Market name of the package (Myket, CafeBazaar, etc.).</param>
    /// <param name="verifierFactory">
    /// A factory method that takes the found package and returns a new ephemeral verifier instance.
    /// </param>
    public async Task<PurchaseVerificationResult<TReward>> VerifyPurchaseAsync(
        string sku,
        string market,
        Func<IShopPackage<TReward>, IPurchaseVerifier<TReward>> verifierFactory)
    {
        var package = GetPackage(sku, market);
        if (package == null)
        {
            return PurchaseVerificationResult<TReward>.Fail("Package not found.", -1);
        }

        var verifier = verifierFactory(package);
        return await verifier.VerifyPurchaseAsync(package);
    }
}