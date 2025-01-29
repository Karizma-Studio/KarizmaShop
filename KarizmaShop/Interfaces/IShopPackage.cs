namespace KarizmaPlatform.Shop.Interfaces;

/// <summary>
/// Represents a generic shop package (item) with a typed reward.
/// Using getter methods to allow for custom annotations on the implementor side.
/// </summary>
/// <typeparam name="TReward">The type of reward given by this package.</typeparam>
public interface IShopPackage<TReward>
{
    /// <summary>
    /// Returns the unique identifier of this package.
    /// </summary>
    string GetId();
    
    /// <summary>
    /// Returns the SKU of this package.
    /// </summary>
    string GetSku();

    /// <summary>
    /// Returns the market name for this package 
    /// (e.g., "Myket", "CafeBazaar", "Test", or "GooglePlay").
    /// </summary>
    string GetMarket();

    /// <summary>
    /// Returns the price of this package.
    /// </summary>
    decimal GetPrice();

    /// <summary>
    /// Returns the typed reward for this package
    /// (e.g., in-game currency, items, time-based bonuses, etc.).
    /// </summary>
    TReward GetRewards();
}