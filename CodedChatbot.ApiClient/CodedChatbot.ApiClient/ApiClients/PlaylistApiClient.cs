using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.Enums.Playlist;
using CoreCodedChatbot.ApiContract.RequestModels.Playlist;
using CoreCodedChatbot.ApiContract.ResponseModels.Playlist;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class PlaylistApiClient : IPlaylistApiClient
    {
        private readonly ILogger<IPlaylistApiClient> _logger;
        private HttpClient _playlistClient;

        public PlaylistApiClient(
            IConfigService configService,
            ISecretService secretService,
            ILogger<IPlaylistApiClient> logger)
        {
            _logger = logger;
            _playlistClient = new HttpClient
            {
                BaseAddress = new Uri(configService.Get<string>("PlaylistApiUrl")),
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", secretService.GetSecret<string>("JwtTokenString"))
                }
            };
        }

        public async Task<EditRequestResponse> EditRequest(EditSongRequest editSongRequest)
        {
            try
            {
                var result = await _playlistClient.PostAsync("EditRequest",
                    HttpClientHelper.GetJsonData(editSongRequest));

                return JsonConvert.DeserializeObject<EditRequestResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<EditRequestResponse>(_logger, e, new object[] {editSongRequest.Username, editSongRequest.CommandText, editSongRequest.IsMod});
            }
        }

        public async Task<GetUserRequestsResponse> GetUserRequests(string username)
        {
            try
            {
                var result = await _playlistClient.PostAsync("GetUserRequests",
                    HttpClientHelper.GetJsonData(username));

                return JsonConvert.DeserializeObject<GetUserRequestsResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetUserRequestsResponse>(_logger, e, new object[] {username});
            }
        }

        public async Task<bool> OpenPlaylist()
        {
            try
            {
                var result = await _playlistClient.GetAsync("OpenPlaylist");

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {});
            }
        }

        public async Task<bool> VeryClosePlaylist()
        {
            try
            {
                var result = await _playlistClient.GetAsync("VeryClosePlaylist");

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {});
            }
        }

        public async Task<bool> ClosePlaylist()
        {
            try
            {
                var result = await _playlistClient.GetAsync("ClosePlaylist");

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {});
            }
        }

        public async Task<PlaylistState> IsPlaylistOpen()
        {
            try
            {
                var result = await _playlistClient.GetAsync("IsPlaylistOpen");

                return JsonConvert.DeserializeObject<PlaylistState>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<PlaylistState>(_logger, e, new object[] {});
            }
        }

        public async Task<bool> ArchiveCurrentRequest()
        {
            try
            {
                var result = await _playlistClient.GetAsync("ArchiveCurrentRequest");

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {});
            }
        }

        public async Task<bool> RemoveRockRequests(RemoveSongRequest removeSongRequest)
        {
            try
            {
                var result = await _playlistClient.PostAsync("RemoveRockRequests",
                    HttpClientHelper.GetJsonData(removeSongRequest));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {removeSongRequest.Username, removeSongRequest.CommandText, removeSongRequest.IsMod});
            }
        }

        public async Task<bool> RemoveSuperVip(RemoveSuperVipRequest removeSuperVipRequest)
        {
            try
            {
                var result = await _playlistClient.PostAsync("RemoveSuperVip",
                    HttpClientHelper.GetJsonData(removeSuperVipRequest));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {removeSuperVipRequest.Username});
            }
        }

        public async Task<AddRequestResponse> AddSong(AddSongRequest addSongRequest)
        {
            try
            {
                var result = await _playlistClient.PostAsync("AddRequest",
                    HttpClientHelper.GetJsonData(addSongRequest));

                return JsonConvert.DeserializeObject<AddRequestResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<AddRequestResponse>(_logger, e, new object[] {addSongRequest.Username, addSongRequest.CommandText, addSongRequest.IsVipRequest});
            }
        }

        public async Task<AddRequestResponse> AddSuperVip(AddSuperVipRequest addSuperVipRequest)
        {
            try
            {
                var result = await _playlistClient.PostAsync("AddSuperRequest",
                    HttpClientHelper.GetJsonData(addSuperVipRequest));

                return JsonConvert.DeserializeObject<AddRequestResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<AddRequestResponse>(_logger, e, new object[] {addSuperVipRequest.Username, addSuperVipRequest.CommandText});
            }
        }

        public async Task<EditRequestResponse> EditSuperVip(EditSuperVipRequest editSuperVipRequest)
        {
            try
            {
                var result = await _playlistClient.PostAsync("EditSuperVipRequest",
                    HttpClientHelper.GetJsonData(editSuperVipRequest));

                return JsonConvert.DeserializeObject<EditRequestResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<EditRequestResponse>(_logger, e, new object[] {editSuperVipRequest.Username, editSuperVipRequest.CommandText});
            }
        }

        public async Task<int> PromoteSong(PromoteSongRequest promoteSongRequest)
        {
            try
            {
                var result = await _playlistClient.PostAsync("PromoteRequest",
                    HttpClientHelper.GetJsonData(promoteSongRequest));

                return JsonConvert.DeserializeObject<int>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<int>(_logger, e, new object[] {promoteSongRequest.Username});
            }
        }

        public async Task<bool> ClearRequests()
        {
            try
            {
                var result = await _playlistClient.GetAsync("ClearRequests");

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {});
            }
        }

        public async Task<GetAllSongsResponse> GetAllCurrentSongRequests()
        {
            try
            {
                var result = await _playlistClient.GetAsync("GetAllCurrentSongRequests");

                return JsonConvert.DeserializeObject<GetAllSongsResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetAllSongsResponse>(_logger, e, new object[] { });
            }
        }

        public async Task<GetRequestByIdResponse> GetRequestById(int songId)
        {
            try
            {
                var result = await _playlistClient.GetAsync($"GetRequestById?songId={songId}");

                return JsonConvert.DeserializeObject<GetRequestByIdResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetRequestByIdResponse>(_logger, e, new object[] { });
            }
        }

        public async Task<bool> ArchiveRequestById(int songId)
        {
            try
            {
                var result =
                    await _playlistClient.PostAsync($"ArchiveRequestById", HttpClientHelper.GetJsonData(songId));

                return JsonConvert.DeserializeObject<bool>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] { });
            }
        }

        public async Task<bool> ArchiveCurrentRequest(int songId)
        {
            try
            {
                var result =
                    await _playlistClient.PostAsync($"ArchiveCurrentRequest", HttpClientHelper.GetJsonData(songId));

                return JsonConvert.DeserializeObject<bool>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] { });
            }
        }

        public async Task<AddRequestResponse> AddWebRequest(AddWebSongRequest addWebSongRequest)
        {
            try
            {
                var result =
                    await _playlistClient.PostAsync("AddWebRequest", HttpClientHelper.GetJsonData(addWebSongRequest));

                return JsonConvert.DeserializeObject<AddRequestResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<AddRequestResponse>(_logger, e, new object[] {addWebSongRequest});
            }
        }

        public async Task<MaxUserRequestsResponse> GetMaxUserRequests()
        {
            try
            {
                var result = await _playlistClient.GetAsync("GetMaxUserRequests");

                return JsonConvert.DeserializeObject<MaxUserRequestsResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<MaxUserRequestsResponse>(_logger, e, new object[] {});
            }
        }

        public async Task<EditRequestResponse> EditWebRequest(EditWebRequestRequestModel editWebRequestRequestModel)
        {
            try
            {
                var result = await _playlistClient.PostAsync("EditWebRequest",
                    HttpClientHelper.GetJsonData(editWebRequestRequestModel));

                return JsonConvert.DeserializeObject<EditRequestResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<EditRequestResponse>(_logger, e, new object[] {editWebRequestRequestModel});
            }
        }

        public async Task<int> PromoteWebRequest(PromoteWebRequestRequestModel promoteWebRequestRequestModel)
        {
            try
            {
                var result = await _playlistClient.PostAsync("PromoteWebRequest",
                    HttpClientHelper.GetJsonData(promoteWebRequestRequestModel));

                return JsonConvert.DeserializeObject<int>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<int>(_logger, e, new object[] {promoteWebRequestRequestModel});
            }
        }

        public async Task<bool> AddRequestToDrive(AddSongToDriveRequest addSongToDriveRequest)
        {
            try
            {
                var result = await _playlistClient.PostAsync("AddRequestToDrive",
                    HttpClientHelper.GetJsonData(addSongToDriveRequest));

                return JsonConvert.DeserializeObject<bool>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {addSongToDriveRequest});
            }
        }
    }
}