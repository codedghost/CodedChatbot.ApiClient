using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.Vip;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface IVipApiClient
    {
        Task<bool> GiftVip(GiftVipRequest giftVipModel);
        Task<bool> ModGiveVip(ModGiveVipRequest modGiveVipModel);
    }
}