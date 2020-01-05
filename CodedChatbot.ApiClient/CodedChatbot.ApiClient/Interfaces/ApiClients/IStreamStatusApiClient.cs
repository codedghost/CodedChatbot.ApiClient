using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.StreamStatus;
using CoreCodedChatbot.ApiContract.ResponseModels.StreamStatus;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface IStreamStatusApiClient
    {
        Task<GetStreamStatusResponse> GetStreamStatus(GetStreamStatusRequest getStreamStatusRequest);
        Task<bool> SaveStreamStatus(PutStreamStatusRequest putStreamStatusRequest);
    }
}