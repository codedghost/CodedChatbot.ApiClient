using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiClient.DataHelper;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using CoreCodedChatbot.ApiContract.RequestModels.GuessingGame;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;

namespace CoreCodedChatbot.ApiClient.ApiClients
{
    public class GuessingGameApiClient : IGuessingGameApiClient
    {
        private readonly ILogger<IGuessingGameApiClient> _logger;
        private HttpClient _guessingGameClient;

        public GuessingGameApiClient(
            IConfigService configService,
            ISecretService secretService,
            ILogger<IGuessingGameApiClient> logger
        )
        {
            _logger = logger;
            _guessingGameClient = new HttpClient
            {
                BaseAddress = new Uri($"{configService.Get<string>("ApiBaseAddress")}/GuessingGame/"),
                DefaultRequestHeaders =
                {
                    Authorization =
                        new AuthenticationHeaderValue("Bearer", secretService.GetSecret<string>("JwtTokenString"))
                }
            };
        }

        public async Task<bool> StartGuessingGame(StartGuessingGameRequest songInfo)
        {
            try
            {
                var result = await _guessingGameClient.PostAsync("StartGuessingGame",
                    HttpClientHelper.GetJsonData(songInfo));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[]{songInfo});
            }
        }

        public async Task<bool> FinishGuessingGame(decimal finalPercentage)
        {
            try
            {
                var result = await _guessingGameClient.PostAsync("FinishGuessingGame",
                    HttpClientHelper.GetJsonData(finalPercentage));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {finalPercentage});
            }
        }

        public async Task<bool> SubmitGuess(SubmitGuessRequest submitGuessModel)
        {
            try
            {
                var result = await _guessingGameClient.PostAsync("SubmitGuess",
                    HttpClientHelper.GetJsonData(submitGuessModel));

                return result.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return HttpClientHelper.LogError<bool>(_logger, e, new object[] {submitGuessModel.Guess, submitGuessModel.Username});
            }
        }
    }
}