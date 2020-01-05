using CoreCodedChatbot.ApiClient.ApiClients;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using Microsoft.Extensions.DependencyInjection;

namespace CoreCodedChatbot.ApiClient
{
    public static class Package
    {
        public static IServiceCollection AddApiClientServices(this IServiceCollection services)
        {
            services.AddTransient<IGuessingGameApiClient, GuessingGameApiClient>();
            services.AddTransient<IPlaylistApiClient, PlaylistApiClient>();
            services.AddTransient<IStreamStatusApiClient, StreamStatusApiClient>();
            services.AddTransient<IVipApiClient, VipApiClient>();

            return services;
        }
    }
}
