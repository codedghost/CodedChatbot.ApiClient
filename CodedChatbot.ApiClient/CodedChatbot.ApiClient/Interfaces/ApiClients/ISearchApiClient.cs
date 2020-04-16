using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.Search;
using CoreCodedChatbot.ApiContract.ResponseModels.Search;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public interface ISearchApiClient
    {
        Task<bool> SaveSearchSynonymRequest(SaveSearchSynonymRequest request);
        Task<SongSearchResponse> SongSearch(SongSearchRequest request);
        Task<SongSearchResponse> FormattedSongSearch(FormattedSongSearchRequest request);
        Task<bool> DownloadToOneDrive(DownloadToOneDriveRequest request);
    }
}