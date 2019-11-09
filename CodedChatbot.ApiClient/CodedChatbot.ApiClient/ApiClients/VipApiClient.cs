using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.Vip;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class VipApiClient : IVipApiClient
    {
        private HttpClient _client;

        public VipApiClient(IConfigService configService, ISecretService secretService)
        {
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
            var result =  await _client.PostAsync("GiftVip",
                   HttpClientHelper.GetJsonData(giftVipModel));

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> ModGiveVip(ModGiveVipRequest modGiveVipModel)
        {
            var result = await _client.PostAsync("ModGiveVip", HttpClientHelper.GetJsonData(modGiveVipModel));

            return result.IsSuccessStatusCode;
        }
    }
}