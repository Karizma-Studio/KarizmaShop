using System.Text;
using KarizmaPlatform.Shop.Application.Interfaces;
using KarizmaPlatform.Shop.Application.Models;
using KarizmaPlatform.Shop.Domain.Interfaces;

namespace KarizmaPlatform.Shop.Application.Verifiers;

/// <summary>
/// Purchase verifier for CafeBazaar, using ephemeral data 
/// (apiSecret, token, packageName, etc.) in the constructor.
/// </summary>
public class CafeBazaarPurchaseVerifier<TReward> : IPurchaseVerifier<TReward>
{
    public string MarketName => "CafeBazaar";

    private readonly HttpClient _httpClient;
    private readonly string _apiSecret;
    private readonly string _packageName;
    private readonly string _purchaseToken;

    public CafeBazaarPurchaseVerifier(
        HttpClient httpClient,
        string apiSecret,
        string packageName,
        string purchaseToken)
    {
        _httpClient = httpClient;
        _apiSecret = apiSecret;
        _packageName = packageName;
        _purchaseToken = purchaseToken;
    }

    public async Task<PurchaseVerificationResult<TReward>> VerifyPurchaseAsync(IShopPackage<TReward> shopPackage)
    {
        if (string.IsNullOrWhiteSpace(_apiSecret))
        {
            return PurchaseVerificationResult<TReward>.Fail("CafeBazaar API secret is empty.", 1);
        }

        if (string.IsNullOrWhiteSpace(_packageName))
        {
            return PurchaseVerificationResult<TReward>.Fail("CafeBazaar package name is empty.", 1);
        }

        // Construct request URL
        var url = $"https://pardakht.cafebazaar.ir/devapi/v2/api/consume/{_packageName}/purchases/";

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("CAFEBAZAAR-PISHKHAN-API-SECRET", _apiSecret);
        request.Content = new StringContent($"{{\"token\":\"{_purchaseToken}\"}}", Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            var code = (int)response.StatusCode;
            return code switch
            {
                404 => PurchaseVerificationResult<TReward>.Fail("Token not verified!", 2),
                400 => PurchaseVerificationResult<TReward>.Fail("Token used before!", 3),
                _ => PurchaseVerificationResult<TReward>.Fail("UnknownError!", 4)
            };
        }

        return PurchaseVerificationResult<TReward>.Success(shopPackage.GetRewards());
    }
}