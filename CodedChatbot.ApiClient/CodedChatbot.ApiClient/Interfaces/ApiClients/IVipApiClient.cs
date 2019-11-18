using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.Vip;
using CoreCodedChatbot.ApiContract.ResponseModels.Vip;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface IVipApiClient
    {
        Task<bool> GiftVip(GiftVipRequest giftVipModel);
        Task<bool> ModGiveVip(ModGiveVipRequest modGiveVipModel);
        Task<DoesUserHaveVipResponseModel> DoesUserHaveVip(DoesUserHaveVipRequestModel doesUserHaveVipRequestModel);
    }
}