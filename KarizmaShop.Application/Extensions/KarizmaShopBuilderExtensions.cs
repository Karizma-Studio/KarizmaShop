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
    /// <typeparam name="TReward">The type of the reward in your packages.</typeparam>
    /// <param name="services">The IServiceCollection.</param>
    /// <param name="configureOptions">An optional action to configure KarizmaShopOptions.</param>
    /// <returns>The IServiceCollection for chaining.</returns>
    public static IServiceCollection AddKarizmaShop<TReward>(
        this IServiceCollection services,
        Action<KarizmaShopOptions>? configureOptions = null)
    {
        if (configureOptions != null)
        {
            services.Configure(configureOptions);
        }
        else
        {
            // Provide some default settings if you like
            services.Configure<KarizmaShopOptions>(opts => { opts.UseTestPackages = false; });
        }

        // Register KarizmaShopService<TReward> as a singleton 
        // or as needed (transient/scoped). Typically singleton is fine 
        // if the package set is not changing frequently.
        services.AddSingleton<KarizmaShopService<TReward>>();

        return services;
    }
}