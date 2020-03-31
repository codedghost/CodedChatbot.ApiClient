using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.Search;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class SearchApiClient : ISearchApiClient
    {
        private readonly ILogger<ISearchApiClient> _logger;
        private HttpClient _searchClient;

        public SearchApiClient(
            IConfigService configService,
            ISecretService secretService,
            ILogger<ISearchApiClient> logger)
        {
            _logger = logger;
            _searchClient = HttpClientHelper.BuildClient(configService, secretService, "Search");
        }

        public async Task<bool> SaveSearchSynonymRequest(SaveSearchSynonymRequest request)
        {
            try
            {
                var result =
                    await _searchClient.PostAsync("SaveSearchSynonymRequest", HttpClientHelper.GetJsonData(request));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] { request.SearchSynonymRequest, request.Username });
            }
        }
    }
}