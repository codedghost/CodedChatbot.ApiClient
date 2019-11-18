using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.Vip;
using CoreCodedChatbot.ApiContract.ResponseModels.Vip;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class VipApiClient : IVipApiClient
    {
        private readonly ILogger<IVipApiClient> _logger;
        private HttpClient _client;

        public VipApiClient(
            IConfigService configService,
            ISecretService secretService, 
            ILogger<IVipApiClient> logger
        )
        {
            _logger = logger;
            _client = new HttpClient
            {
                BaseAddress = new Uri(configService.Get<string>("VipApiUrl")),
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", secretService.GetSecret<string>("JwtTokenString"))
                }
            };
        }

        public async Task<bool> GiftVip(GiftVipRequest giftVipModel)
        {
            try
            {
                var result = await _client.PostAsync("GiftVip",
                    HttpClientHelper.GetJsonData(giftVipModel));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {giftVipModel.DonorUsername, giftVipModel.ReceiverUsername});
            }
        }

        public async Task<bool> ModGiveVip(ModGiveVipRequest modGiveVipModel)
        {
            try
            {
                var result = await _client.PostAsync("ModGiveVip", HttpClientHelper.GetJsonData(modGiveVipModel));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {modGiveVipModel.ReceivingUsername, modGiveVipModel.VipsToGive});
            }
        }

        public async Task<DoesUserHaveVipResponseModel> DoesUserHaveVip(
            DoesUserHaveVipRequestModel doesUserHaveVipRequestModel)
        {
            try
            {
                var result = await _client.GetAsync($"DoesUserHaveVip?username={doesUserHaveVipRequestModel.Username}");

                return JsonConvert.DeserializeObject<DoesUserHaveVipResponseModel>(
                    await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<DoesUserHaveVipResponseModel>(_logger, e, new object[] {doesUserHaveVipRequestModel.Username});
            }
        }
    }
}