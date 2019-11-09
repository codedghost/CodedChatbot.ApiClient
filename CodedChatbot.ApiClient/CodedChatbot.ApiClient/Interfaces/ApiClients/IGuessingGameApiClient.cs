using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.RequestModels.GuessingGame;

namespace CoreCodedChatbot.ApiClient.Interfaces.ApiClients
{
    public interface IGuessingGameApiClient
    {
        Task<bool> StartGuessingGame(StartGuessingGameRequest songInfo);
        Task<bool> FinishGuessingGame(decimal finalPercentage);
        Task<bool> SubmitGuess(SubmitGuessRequest submitGuessModel);
    }
}