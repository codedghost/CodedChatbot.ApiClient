using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.ChannelRewards;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface IChannelRewardsClient
    {
        Task<bool> CreateOrUpdate(CreateOrUpdateChannelRewardRequest request);
        Task<bool> StoreRedemption(StoreRewardRedemptionRequest request);
    }
}