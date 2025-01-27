using System.Text;
using KarizmaPlatform.Shop.Interfaces;
using KarizmaPlatform.Shop.Models;

namespace KarizmaPlatform.Shop.Verifiers;

/// <summary>
    /// Purchase verifier for Myket, using ephemeral data 
    /// (accessToken, token, packageName, etc.) in the constructor.
    /// </summary>
    public class MyketPurchaseVerifier<TReward> : IPurchaseVerifier<TReward>
    {
        public string MarketName => "Myket";

        private readonly HttpClient _httpClient;
        private readonly string _accessToken;
        private readonly string _packageName;
        private readonly string _purchaseToken;

        public MyketPurchaseVerifier(
            HttpClient httpClient,
            string accessToken,
            string packageName,
            string purchaseToken)
        {
            _httpClient = httpClient;
            _accessToken = accessToken;
            _packageName = packageName;
            _purchaseToken = purchaseToken;
        }

        public async Task<PurchaseVerificationResult<TReward>> VerifyPurchaseAsync(IShopPackage<TReward> shopPackage)
        {
            if (string.IsNullOrWhiteSpace(_accessToken))
            {
                return PurchaseVerificationResult<TReward>.Fail("Myket access token is empty.", 1);
            }
            if (string.IsNullOrWhiteSpace(_packageName))
            {
                return PurchaseVerificationResult<TReward>.Fail("Myket package name is empty.", 1);
            }

            // Construct request URL
            var url = $"https://developer.myket.ir/api/partners/applications/{_packageName}" +
                      $"/purchases/products/{shopPackage.GetSku()}/tokens/{_purchaseToken}/consume";

            using var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Add("X-Access-Token", _accessToken);
            request.Content = new StringContent($"{{\"tokenId\":\"{_purchaseToken}\"}}", Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var code = (int)response.StatusCode;
                return code switch
                {
                    404 => PurchaseVerificationResult<TReward>.Fail("Token not verified!", 2),
                    400 => PurchaseVerificationResult<TReward>.Fail("Token used before!", 3),
                    _   => PurchaseVerificationResult<TReward>.Fail("UnknownError!", 4),
                };
            }

            return PurchaseVerificationResult<TReward>.Success(shopPackage.GetRewards());
        }
    }