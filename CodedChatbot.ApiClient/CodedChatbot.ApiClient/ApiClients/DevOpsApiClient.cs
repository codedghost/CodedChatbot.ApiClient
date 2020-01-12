using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.DevOps;
using CoreCodedChatbot.ApiContract.ResponseModels.DevOps;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class DevOpsApiClient : IDevOpsApiClient
    {
        private ILogger<IDevOpsApiClient> _logger;
        private HttpClient _client;

        public DevOpsApiClient(
            IConfigService configService,
            ISecretService secretService,
            ILogger<IDevOpsApiClient> logger
        )
        {
            _logger = logger;
            _client = HttpClientHelper.BuildClient(configService, secretService, "DevOps");
        }

        public async Task<GetWorkItemByIdResponse> GetWorkItemById(int id)
        {
            try
            {
                var result = await _client.GetAsync($"GetWorkItemsById/{id}");

                return JsonConvert.DeserializeObject<GetWorkItemByIdResponse>(await result.Content.ReadAsStringAsync(), HttpClientHelper.GetJsonSerializerSettings());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetWorkItemByIdResponse>(_logger, e, new object[] {id});
            }
        }

        public async Task<GetAllCurrentWorkItemsResponse> GetAllCurrentWorkItems()
        {
            try
            {
                var result = await _client.GetAsync("GetAllCurrentWorkItems");

                return JsonConvert.DeserializeObject<GetAllCurrentWorkItemsResponse>(
                    await result.Content.ReadAsStringAsync(), HttpClientHelper.GetJsonSerializerSettings());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetAllCurrentWorkItemsResponse>(_logger, e, new object[] { });
            }
        }

        public async Task<bool> RaiseBug(RaiseBugRequest raiseBugRequest)
        {
            try
            {
                var result = await _client.PutAsync("RaiseBug", HttpClientHelper.GetJsonData(raiseBugRequest));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e,
                    new object[]
                    {
                        raiseBugRequest.TwitchUsername, raiseBugRequest.BugInfo.Title,
                        raiseBugRequest.BugInfo.SystemInfo, raiseBugRequest.BugInfo.ReproSteps,
                        raiseBugRequest.BugInfo.AcceptanceCriteria
                    });
            }
        }

        public async Task<GetAllBacklogWorkItemsResponse> GetAllBacklogWorkItems()
        {
            try
            {
                var result = await _client.GetAsync("GetAllBacklogWorkItems");

                return JsonConvert.DeserializeObject<GetAllBacklogWorkItemsResponse>(
                    await result.Content.ReadAsStringAsync(), HttpClientHelper.GetJsonSerializerSettings());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetAllBacklogWorkItemsResponse>(_logger, e, new object[] { });
            }
        }
    }
}