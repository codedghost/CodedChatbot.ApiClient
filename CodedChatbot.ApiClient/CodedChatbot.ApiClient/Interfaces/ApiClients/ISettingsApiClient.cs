using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.Settings;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface ISettingsApiClient
    {
        Task<bool> UpdateSettings(UpdateSettingsRequest request);
    }
}