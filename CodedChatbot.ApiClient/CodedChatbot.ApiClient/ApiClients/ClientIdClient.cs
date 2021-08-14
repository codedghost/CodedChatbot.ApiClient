using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.ClientId;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class ClientIdClient : IClientIdClient
    {
        private readonly ILogger<IClientIdClient> _logger;
        private HttpClient _clientIdClient;

        public ClientIdClient(
            IConfigService configService,
            ISecretService secretService,
            ILogger<IClientIdClient> logger
        )
        {
            _logger = logger;
            _clientIdClient = HttpClientHelper.BuildClient(configService, secretService, "ClientId");
        }

        public async Task Set(SetClientIdRequestModel request)
        {
            try
            {
                await _clientIdClient.PostAsync("ClientId", HttpClientHelper.GetJsonData(request));
            }
            catch (Exception e)
            {
                HttpClientHelper.LogError<bool>(_logger, e, new object[] { request.HubType, request.Username, request.ClientId });
            }
        }

        public async Task Remove(RemoveClientIdRequestModel request)
        {
            try
            {
                await _clientIdClient.DeleteAsync($"ClientId?hubType={request.HubType}&clientId={request.ClientId}");
            }
            catch (Exception e)
            {
                HttpClientHelper.LogError<bool>(_logger, e, new object[] {request.HubType, request.ClientId});
            }
        }
    }
}