using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.Moderation;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public interface IModerationApiClient
    {
        Task<bool> TransferUserAccount(TransferUserAccountRequest request);
        Task<bool> IsUserMod(string username);
    }
}