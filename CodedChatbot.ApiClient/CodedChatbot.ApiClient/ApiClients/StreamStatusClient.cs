using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.StreamStatus;
using CoreCodedChatbot.ApiContract.ResponseModels.StreamStatus;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Newtonsoft.Json;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class StreamStatusClient : IStreamStatusClient
    {
        private HttpClient _streamStatusClient;

        public StreamStatusClient(IConfigService configService, ISecretService secretService)
        {
            _streamStatusClient = new HttpClient
            {
                BaseAddress = new Uri(configService.Get<string>("StreamStatusApiUrl")),
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", secretService.GetSecret<string>("JwtTokenString"))
                }
            };
        }

        public async Task<GetStreamStatusResponse> GetStreamStatus(GetStreamStatusRequest getStreamStatusRequest)
        {
            var result = await _streamStatusClient.GetAsync($"GetStreamStatus?broadcasterUsername={getStreamStatusRequest.BroadcasterUsername}");

            return JsonConvert.DeserializeObject<GetStreamStatusResponse>(await result.Content.ReadAsStringAsync());
        }

        public async Task<bool> SaveStreamStatus(PutStreamStatusRequest putStreamStatusRequest)
        {
            var result = await _streamStatusClient.PutAsync("PuStreamStatus",
                HttpClientHelper.GetJsonData(putStreamStatusRequest));

            return result.IsSuccessStatusCode;
        }
    }
}