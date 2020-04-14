using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.CustomChatCommands;
using CoreCodedChatbot.ApiContract.ResponseModels.CustomChatCommands;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public interface ICustomChatCommandsClient
    {
        Task<GetCommandTextResponse> GetCommandText(string keyword);
        Task<GetCommandHelpTextResponse> GetCommandHelpText(string keyword);
        Task<bool> AddCommand(AddCommandRequest request);
    }
}