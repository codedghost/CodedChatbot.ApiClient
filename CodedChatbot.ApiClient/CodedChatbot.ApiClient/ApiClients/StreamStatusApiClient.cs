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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class StreamStatusApiClient : IStreamStatusApiClient
    {
        private readonly ILogger<IStreamStatusApiClient> _logger;
        private readonly HttpClient _streamStatusClient;

        public StreamStatusApiClient(
            IConfigService configService,
            ISecretService secretService,
            ILogger<IStreamStatusApiClient> logger
        )
        {
            _logger = logger;
            _streamStatusClient = HttpClientHelper.BuildClient(configService, secretService, "StreamStatus");
        }

        public async Task<GetStreamStatusResponse> GetStreamStatus(GetStreamStatusRequest getStreamStatusRequest)
        {
            try
            {
                var result =
                    await _streamStatusClient.GetAsync(
                        $"GetStreamStatus?broadcasterUsername={getStreamStatusRequest.BroadcasterUsername}");

                return JsonConvert.DeserializeObject<GetStreamStatusResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetStreamStatusResponse>(_logger, e, new object[] {getStreamStatusRequest.BroadcasterUsername});
            }
        }

        public async Task<bool> SaveStreamStatus(PutStreamStatusRequest putStreamStatusRequest)
        {
            try
            {
                var result = await _streamStatusClient.PutAsync("PuStreamStatus",
                    HttpClientHelper.GetJsonData(putStreamStatusRequest));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {putStreamStatusRequest.BroadcasterUsername, putStreamStatusRequest.IsOnline});
            }
        }
    }
}