using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.ChannelRewards;
using CoreCodedChatbot.ApiContract.ResponseModels.ChannelRewards;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface IChannelRewardsClient
    {
        Task<bool> CreateOrUpdate(CreateOrUpdateChannelRewardRequest request);
        Task<StoreRedemptionResponse> StoreRedemption(StoreRewardRedemptionRequest request);
    }
}