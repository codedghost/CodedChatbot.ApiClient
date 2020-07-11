using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.ChannelRewards;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;

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

        public async Task<bool> StoreRedemption(StoreRewardRedemptionRequest request)
        {
            try
            {
                var result =
                    await _channelRewardsClient.PostAsync("StoreRedemption", HttpClientHelper.GetJsonData(request));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {request.ChannelRewardId, request.RedeemedBy});
            }
        }
    }
}