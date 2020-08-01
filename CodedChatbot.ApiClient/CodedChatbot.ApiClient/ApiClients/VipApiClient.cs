using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.Vip;
using CoreCodedChatbot.ApiContract.ResponseModels.Playlist;
using CoreCodedChatbot.ApiContract.ResponseModels.Vip;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class VipApiClient : IVipApiClient
    {
        private readonly ILogger<IVipApiClient> _logger;
        private HttpClient _client;

        public VipApiClient(
            IConfigService configService,
            ISecretService secretService, 
            ILogger<IVipApiClient> logger
        )
        {
            _logger = logger;
            _client = HttpClientHelper.BuildClient(configService, secretService, "Vip");
        }

        public async Task<bool> GiftVip(GiftVipRequest giftVipModel)
        {
            try
            {
                var result = await _client.PostAsync("GiftVip",
                    HttpClientHelper.GetJsonData(giftVipModel));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {giftVipModel.DonorUsername, giftVipModel.ReceiverUsername});
            }
        }

        public async Task<bool> ModGiveVip(ModGiveVipRequest modGiveVipModel)
        {
            try
            {
                var result = await _client.PostAsync("ModGiveVip", HttpClientHelper.GetJsonData(modGiveVipModel));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {modGiveVipModel.ReceivingUsername, modGiveVipModel.VipsToGive});
            }
        }

        public async Task<DoesUserHaveVipResponseModel> DoesUserHaveVip(
            DoesUserHaveVipRequestModel doesUserHaveVipRequestModel)
        {
            try
            {
                var result = await _client.GetAsync($"DoesUserHaveVip?username={doesUserHaveVipRequestModel.Username}");

                return JsonConvert.DeserializeObject<DoesUserHaveVipResponseModel>(
                    await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<DoesUserHaveVipResponseModel>(_logger, e, new object[] {doesUserHaveVipRequestModel.Username});
            }
        }

        public async Task<DoesUserHaveSuperVipResponseModel> DoesUserHaveSuperVip(
            DoesUserHaveSuperVipRequestModel doesUserHaveSuperVipRequestModel)
        {
            try
            {
                var result =
                    await _client.GetAsync(
                        $"DoesUserHaveSuperVip?username={doesUserHaveSuperVipRequestModel.Username}");

                return JsonConvert.DeserializeObject<DoesUserHaveSuperVipResponseModel>(
                    await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<DoesUserHaveSuperVipResponseModel>(_logger, e,
                    new object[] {doesUserHaveSuperVipRequestModel.Username});
            }
        }

        public async Task<IsSuperVipInQueueResponse> IsSuperVipInQueue()
        {
            try
            {
                var result = await _client.GetAsync("IsSuperVipInQueue");

                return JsonConvert.DeserializeObject<IsSuperVipInQueueResponse>(
                    await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<IsSuperVipInQueueResponse>(_logger, e, new object[] { });
            }
        }

        public async Task<GetUserVipCountResponse> GetUserVipCount(GetUserVipCountRequest request)
        {
            try
            {
                var result = await _client.GetAsync($"GetUserVipCount?username={request.Username}");

                return JsonConvert.DeserializeObject<GetUserVipCountResponse>(
                    await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetUserVipCountResponse>(_logger, e, new[] {request.Username});
            }
        }

        public async Task<GetUserByteCountResponse> GetUserByteCount(string username)
        {
            try
            {
                var result = await _client.GetAsync($"GetUserByteCount?username={username}");

                return JsonConvert.DeserializeObject<GetUserByteCountResponse>(
                    await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetUserByteCountResponse>(_logger, e, new object[] {username});
            }
        }

        public async Task<bool> GiveSubscriptionVips(GiveSubscriptionVipsRequest request)
        {
            try
            {
                var result = await _client.PostAsync("GiveSubscriptionVips", HttpClientHelper.GetJsonData(request));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[]
                {
                    string.Join(", ",
                        request.UserSubDetails.Select(d =>
                            $"{d.Username}, Months: {d.TotalSubMonths}, Streak: {d.SubStreak}, Tier: {d.SubscriptionTier}"))
                });
            }
        }

        public async Task<bool> UpdateBitsDropped(UpdateTotalBitsDroppedRequest request)
        {
            try
            {
                var result = await _client.PostAsync("UpdateBitsDropped", HttpClientHelper.GetJsonData(request));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] { request.Username, request.TotalBitsDropped });
            }
        }

        public async Task<ByteConversionResponse> ConvertBytes(ConvertVipsRequest request)
        {
            try
            {
                var result = await _client.PostAsync("ConvertBytes", HttpClientHelper.GetJsonData(request));

                return JsonConvert.DeserializeObject<ByteConversionResponse>(
                    await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<ByteConversionResponse>(_logger, e,
                    new object[] {request.Username, request.NumberOfBytes});
            }
        }

        public async Task<ByteConversionResponse> ConvertAllBytes(ConvertAllVipsRequest request)
        {
            try
            {
                var result = await _client.PostAsync("ConvertAllBytes", HttpClientHelper.GetJsonData(request));

                return JsonConvert.DeserializeObject<ByteConversionResponse>(
                    await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<ByteConversionResponse>(_logger, e, new object[] { request.Username});
            }
        }

        public async Task<bool> GiveGiftSubBytes(GiveGiftSubBytesRequest request)
        {
            try
            {
                var result = await _client.PostAsync("GiveGiftSubBytes", HttpClientHelper.GetJsonData(request));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {request.Username});
            }
        }

        public async Task<GetGiftedVipsResponse> GetGiftedVips(string username)
        {
            try
            {
                var result = await _client.GetAsync($"GetGiftedVips?username={username}");

                return JsonConvert.DeserializeObject<GetGiftedVipsResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetGiftedVipsResponse>(_logger, e, new object[] {username});
            }
        }
    }
}