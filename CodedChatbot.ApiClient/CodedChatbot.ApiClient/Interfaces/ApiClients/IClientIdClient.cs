using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.ClientId;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface IClientIdClient
    {
        void Set(SetClientIdRequestModel request);
        void Remove(RemoveClientIdRequestModel request);
    }
}