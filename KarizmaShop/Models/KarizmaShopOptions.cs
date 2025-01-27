namespace KarizmaPlatform.Shop.Models;

/// <summary>
/// Configuration options for the KarizmaShopService. 
/// (Optional usage, can store default behaviors or environment config.)
/// </summary>
public class KarizmaShopOptions
{
    /// <summary>
    /// Example config property (not mandatory). 
    /// Could define defaults, API keys, or how to load packages, etc.
    /// </summary>
    public bool UseTestPackages { get; set; } = false;
}