using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.ClientId;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface IClientIdClient
    {
        Task Set(SetClientIdRequestModel request);
        Task Remove(RemoveClientIdRequestModel request);
    }
}