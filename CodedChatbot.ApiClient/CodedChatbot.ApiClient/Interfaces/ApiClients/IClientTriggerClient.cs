using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.ClientTrigger;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface IClientTriggerClient
    {
        Task<bool> CheckBackgroundSong(CheckBackgroundSongRequest request);
        Task<bool> SendBackgroundSongResult(SendBackgroundSongResultRequest request);
    }
}