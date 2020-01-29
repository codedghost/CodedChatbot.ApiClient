using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.Quotes;
using CoreCodedChatbot.ApiContract.ResponseModels.Quotes;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface IQuoteApiClient
    {
        Task<AddQuoteResponse> AddQuote(AddQuoteRequest request);
        Task<bool> EditQuote(EditQuoteRequest request);
        Task<bool> RemoveQuote(RemoveQuoteRequest request);
        Task<GetQuoteResponse> GetQuote(GetQuoteRequest request);
    }
}