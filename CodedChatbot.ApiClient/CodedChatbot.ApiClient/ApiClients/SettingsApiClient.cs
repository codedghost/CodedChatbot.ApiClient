using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.Settings;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class SettingsApiClient : ISettingsApiClient
    {
        private readonly ILogger<ISearchApiClient> _logger;
        private HttpClient _settingsClient;

        public SettingsApiClient(
            IConfigService configService,
            ISecretService secretService,
            ILogger<ISearchApiClient> logger)
        {
            _logger = logger;
            _settingsClient = HttpClientHelper.BuildClient(configService, secretService, "Settings");
        }

        public async Task<bool> UpdateSettings(UpdateSettingsRequest request)
        {
            try
            {
                var result = await _settingsClient.PostAsync("Update",
                    HttpClientHelper.GetJsonData(request));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] { request.Key, request.Value });
            }
        }
    }
}