using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.Enums.ChannelRewards;
using CoreCodedChatbot.ApiContract.RequestModels.ChannelRewards;
using CoreCodedChatbot.ApiContract.ResponseModels.ChannelRewards;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class ChannelRewardsClient : IChannelRewardsClient
    {
        private readonly ILogger<IChannelRewardsClient> _logger;
        private HttpClient _channelRewardsClient;

        public ChannelRewardsClient(
            IConfigService configService,
            ISecretService secretService,
            ILogger<IChannelRewardsClient> logger
            )
        {
            _logger = logger;
            _channelRewardsClient = HttpClientHelper.BuildClient(configService, secretService, "ChannelRewards");
        }

        public async Task<bool> CreateOrUpdate(CreateOrUpdateChannelRewardRequest request)
        {
            try
            {
                var result =
                    await _channelRewardsClient.PostAsync("CreateOrUpdate", HttpClientHelper.GetJsonData(request));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {request.ChannelRewardId, request.RewardTitle, request.RewardDescription});
            }
        }

        public async Task<StoreRedemptionResponse> StoreRedemption(StoreRewardRedemptionRequest request)
        {
            try
            {
                var result =
                    await _channelRewardsClient.PostAsync("StoreRedemption", HttpClientHelper.GetJsonData(request));

                return JsonConvert.DeserializeObject<StoreRedemptionResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<StoreRedemptionResponse>(_logger, e, new object[] {request.ChannelRewardId, request.RedeemedBy});
            }
        }
    }
}