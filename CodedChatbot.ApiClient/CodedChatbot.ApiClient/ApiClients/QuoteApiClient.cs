using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.Quotes;
using CoreCodedChatbot.ApiContract.ResponseModels.Playlist;
using CoreCodedChatbot.ApiContract.ResponseModels.Quotes;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class QuoteApiClient : IQuoteApiClient
    {
        private readonly ILogger<IQuoteApiClient> _logger;
        private HttpClient _quoteClient;

        public QuoteApiClient(
            IConfigService configService,
            ISecretService secretService,
            ILogger<IQuoteApiClient> logger)
        {
            _logger = logger;
            _quoteClient = HttpClientHelper.BuildClient(configService, secretService, "Quote");
        }

        public async Task<AddQuoteResponse> AddQuote(AddQuoteRequest request)
        {
            try
            {
                var result = await _quoteClient.PutAsync("AddQuote",
                    HttpClientHelper.GetJsonData(request));

                return JsonConvert.DeserializeObject<AddQuoteResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<AddQuoteResponse>(_logger, e, new object[] { request.Username, request.QuoteText });
            }
        }

        public async Task<bool> EditQuote(EditQuoteRequest request)
        {
            try
            {
                var result = await _quoteClient.PostAsync("EditQuote",
                    HttpClientHelper.GetJsonData(request));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e,
                    new object[] {request.Username, request.QuoteId, request.QuoteText});
            }
        }

        public async Task<bool> RemoveQuote(RemoveQuoteRequest request)
        {
            try
            {
                var result = await _quoteClient.PostAsync("RemoveQuote",
                    HttpClientHelper.GetJsonData(request));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e,
                    new object[] {request.Username, request.QuoteId});
            }
        }

        public async Task<GetQuoteResponse> GetQuote(GetQuoteRequest request)
        {
            try
            {
                var result = await _quoteClient.GetAsync("GetQuote" + (request.QuoteId.HasValue
                                                             ? $"?quoteId={request.QuoteId.Value}"
                                                             : string.Empty));

                return JsonConvert.DeserializeObject<GetQuoteResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetQuoteResponse>(_logger, e, new object[] {request.QuoteId});
            }
        }

        public async Task<GetQuotesResponse> GetQuotes()
        {
            try
            {
                var result = await _quoteClient.GetAsync("GetQuotes");

                return JsonConvert.DeserializeObject<GetQuotesResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetQuotesResponse>(_logger, e, new object[] {});
            }
        }
    }
}