using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.ClientTrigger;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class ClientTriggerClient : IClientTriggerClient
    {
        private readonly ILogger<IClientTriggerClient> _logger;
        private HttpClient _clientTriggerClient;

        public ClientTriggerClient(
            IConfigService configService,
            ISecretService secretService,
            ILogger<IClientTriggerClient> logger
        )
        {
            _logger = logger;
            _clientTriggerClient = HttpClientHelper.BuildClient(configService, secretService, "ClientTrigger");
        }
        public async Task<bool> CheckBackgroundSong(CheckBackgroundSongRequest request)
        {
            try
            {
                var result =
                    await _clientTriggerClient.PostAsync("CheckBackgroundSong", HttpClientHelper.GetJsonData(request));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] { request.Username });
            }
        }
    }
}