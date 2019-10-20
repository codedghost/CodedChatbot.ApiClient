using System;
using System.Collections.Generic;
using System.Text;
using CoreCodedChatbot.ApiClient.ApiClients;
using CoreCodedChatbot.ApiClient.Interfaces.ApiClients;
using Microsoft.Extensions.DependencyInjection;

namespace CoreCodedChatbot.ApiClient
{
    public static class Package
    {
        public static ServiceCollection AddApiClientServices(this ServiceCollection services)
        {
            services.AddTransient<IGuessingGameApiClient, GuessingGameApiClient>();
            services.AddTransient<IPlaylistApiClient, PlaylistApiClient>();
            services.AddTransient<IStreamStatusClient, StreamStatusClient>();
            services.AddTransient<IVipApiClient, VipApiClient>();

            return services;
        }
    }
}
