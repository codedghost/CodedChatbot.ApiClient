using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.Enums.Playlist;
using CoreCodedChatbot.ApiContract.RequestModels.Playlist;
using CoreCodedChatbot.ApiContract.ResponseModels.Playlist;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface IPlaylistApiClient
    {
        Task<EditRequestResponse> EditRequest(EditSongRequest editSongRequest);
        Task<GetUserRequestsResponse> GetUserRequests(string username);
        Task<bool> OpenPlaylist();
        Task<bool> VeryClosePlaylist();
        Task<bool> ClosePlaylist();
        Task<PlaylistState> IsPlaylistOpen();
        Task<bool> ArchiveCurrentRequest();
        Task<bool> RemoveRockRequests(RemoveSongRequest removeSongRequest);
        Task<bool> RemoveSuperVip(RemoveSuperVipRequest removeSuperVipRequest);
        Task<AddRequestResponse> AddSong(AddSongRequest addSongRequest);
        Task<AddRequestResponse> AddSuperVip(AddSuperVipRequest addSuperVipRequest);
        Task<EditRequestResponse> EditSuperVip(EditSuperVipRequest editSuperVipRequest);
        Task<int> PromoteSong(PromoteSongRequest promoteSongRequest);
        Task<bool> ClearRequests();
        Task<GetAllSongsResponse> GetAllCurrentSongRequests();
        Task<GetRequestByIdResponse> GetRequestById(int songId);
        Task<bool> ArchiveRequestById(int songId);
        Task<bool> ArchiveCurrentRequest(int songId);
        Task<AddRequestResponse> AddWebRequest(AddWebSongRequest addWebSongRequest);
        Task<MaxUserRequestsResponse> GetMaxUserRequests();
        Task<EditWebRequestResponse> EditWebRequest(EditWebRequestRequestModel editWebRequestRequestModel);
        Task<PromoteWebRequestResponseModel> PromoteWebRequest(PromoteWebRequestRequestModel promoteWebRequestRequestModel);
        Task<bool> AddRequestToDrive(AddSongToDriveRequest addSongToDriveRequest);
        Task<GetCurrentSongRequestResponse> GetCurrentSongRequest();
    }
}