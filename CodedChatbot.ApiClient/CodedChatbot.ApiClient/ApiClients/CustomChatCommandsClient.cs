using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.CustomChatCommands;
using CoreCodedChatbot.ApiContract.ResponseModels.CustomChatCommands;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class CustomChatCommandsClient : ICustomChatCommandsClient
    {
        private ILogger<ICustomChatCommandsClient> _logger;
        private HttpClient _client;

        public CustomChatCommandsClient(
            IConfigService configService,
            ISecretService secretService,
            ILogger<ICustomChatCommandsClient> logger
        )
        {
            _logger = logger;
            _client = HttpClientHelper.BuildClient(configService, secretService, "CustomChatCommands");
        }

        public async Task<GetCommandTextResponse> GetCommandText(string keyword)
        {
            try
            {
                var result = await _client.GetAsync($"GetCommandText?keyword={keyword}");

                return JsonConvert.DeserializeObject<GetCommandTextResponse>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetCommandTextResponse>(_logger, e, new object[] { keyword });
            }
        }

        public async Task<GetCommandHelpTextResponse> GetCommandHelpText(string keyword)
        {
            try
            {
                var result = await _client.GetAsync($"GetCommandHelpText?keyword={keyword}");

                return JsonConvert.DeserializeObject<GetCommandHelpTextResponse>(
                    await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<GetCommandHelpTextResponse>(_logger, e, new object[] {keyword});
            }
        }

        public async Task<bool> AddCommand(AddCommandRequest request)
        {
            try
            {
                var result = await _client.PostAsync("AddCommand", HttpClientHelper.GetJsonData(request));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e,
                    new object[] {request.Aliases, request.InformationText, request.HelpText, request.Username});
            }
        }
    }
}