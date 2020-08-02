using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.Moderation;
using CoreCodedChatbot.ApiContract.ResponseModels.Playlist;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class ModerationApiClient : IModerationApiClient
    {
        private readonly ILogger<IModerationApiClient> _logger;
        private HttpClient _moderationClient;

        public ModerationApiClient(
            IConfigService configService,
            ISecretService secretService,
            ILogger<IModerationApiClient> logger)
        {
            _logger = logger;
            _moderationClient = HttpClientHelper.BuildClient(configService, secretService, "Moderation");
        }

        public async Task<bool> TransferUserAccount(TransferUserAccountRequest request)
        {
            try
            {
                var result = await _moderationClient.PostAsync("TransferUserAccount",
                    HttpClientHelper.GetJsonData(request));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] { request.RequestingModerator, request.OldUsername, request.NewUsername });
            }
        }

        public async Task<bool> IsUserMod(string username)
        {
            try
            {
                var result = await _moderationClient.GetAsync($"IsMod?{username}");

                return result.IsSuccessStatusCode &&
                       JsonConvert.DeserializeObject<bool>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {username});
            }
        }
    }
}