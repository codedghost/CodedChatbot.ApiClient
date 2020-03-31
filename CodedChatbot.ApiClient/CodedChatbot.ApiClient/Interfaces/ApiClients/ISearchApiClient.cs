using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.Search;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public interface ISearchApiClient
    {
        Task<bool> SaveSearchSynonymRequest(SaveSearchSynonymRequest request);
    }
}