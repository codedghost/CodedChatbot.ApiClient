using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Library.Helpers;
using CoreCodedChatbot.Library.Interfaces.Services;
using CoreCodedChatbot.Library.Models.ApiRequest.Vip;
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

        public async Task<bool> GiftVip(GiftVipModel giftVipModel)
        {
            var result =  await _client.PostAsync("GiftVip",
                   HttpClientHelper.GetJsonData(giftVipModel));

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> ModGiveVip(ModGiveVipModel modGiveVipModel)
        {
            var result = await _client.PostAsync("ModGiveVip", HttpClientHelper.GetJsonData(modGiveVipModel));

            return result.IsSuccessStatusCode;
        }
    }
}