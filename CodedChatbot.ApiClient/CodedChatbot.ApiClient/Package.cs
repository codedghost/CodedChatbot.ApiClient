﻿using CoreCodedChatbot.ApiClient.ApiClients;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using Microsoft.Extensions.DependencyInjection;

namespace CoreCodedChatbot.ApiClient
{
    public static class Package
    {
        public static IServiceCollection AddApiClientServices(this IServiceCollection services)
        {
            services.AddSingleton<IDevOpsApiClient, DevOpsApiClient>();
            services.AddSingleton<IGuessingGameApiClient, GuessingGameApiClient>();
            services.AddSingleton<IModerationApiClient, ModerationApiClient>();
            services.AddSingleton<IPlaylistApiClient, PlaylistApiClient>();
            services.AddSingleton<IQuoteApiClient, QuoteApiClient>();
            services.AddSingleton<ISearchApiClient, SearchApiClient>();
            services.AddSingleton<IStreamStatusApiClient, StreamStatusApiClient>();
            services.AddSingleton<IVipApiClient, VipApiClient>();

            return services;
        }
    }
}
