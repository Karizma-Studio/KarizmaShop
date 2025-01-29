using KarizmaPlatform.Shop.Application.Models;
using KarizmaPlatform.Shop.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KarizmaPlatform.Shop.Application.Extensions;

/// <summary>
/// Extension method to register KarizmaShop services in DI.
/// </summary>
public static class KarizmaShopBuilderExtensions
{
    /// <summary>
    /// Registers KarizmaShopService<TReward> in the IServiceCollection.
    /// Optionally configures KarizmaShopOptions.
    /// 
    /// Example usage in Startup/Program.cs:
    /// services.AddKarizmaShop<MyReward>(opts => { opts.UseTestPackages = true; });
    /// </summary>
    public static IServiceCollection AddKarizmaShop<TReward>(this IServiceCollection services)
    {
        services.AddSingleton<KarizmaShopService<TReward>>();
        return services;
    }
}