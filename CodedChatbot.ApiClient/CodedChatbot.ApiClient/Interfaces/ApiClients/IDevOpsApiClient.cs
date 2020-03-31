using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.DevOps;
using CoreCodedChatbot.ApiContract.ResponseModels.DevOps;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface IDevOpsApiClient
    {
        Task<GetWorkItemByIdResponse> GetWorkItemById(int id);
        Task<GetAllCurrentWorkItemsResponse> GetAllCurrentWorkItems();
        Task<bool> RaiseBug(RaiseBugRequest raiseBugRequest);
        Task<GetAllBacklogWorkItemsResponse> GetAllBacklogWorkItems();
        Task<bool> PracticeSongRequest(PracticeSongRequest practiceSongRequest);
    }
}