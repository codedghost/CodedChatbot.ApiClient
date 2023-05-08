using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CoreCodedChatbot.ApiContract.ResponseModels.Quotes;
using CoreCodedChatbot.Config;
using CoreCodedChatbot.Secrets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreCodedChatbot.ApiClient.DataHelper
{
    public static class HttpClientHelper
    {
        public static ByteArrayContent GetJsonData(object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);
            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(jsonData));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }

        public static T LogError<T>(ILogger logger, Exception e, object[] args)
        {
            var argsString = string.Join(',', args.Select(a => $"{nameof(a)}: {a}"));

            logger.LogError(e, $"Error encountered in Api Client Method. Data - {argsString}");
            return (T) default;
        }

        public static HttpClient BuildClient(IConfigService configService, ISecretService secretService, string controllerName)
        {
            return new HttpClient
            {
                BaseAddress = new Uri($"{configService.Get<string>("ApiBaseAddress").Trim('/')}/{controllerName}/"),
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", secretService.GetSecret<string>("JwtTokenString"))
                }
            };
        }

        public static JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public static async Task<TResult> GetAsync<TResult>(this HttpClient client, string requestUri, ILogger logger)
        {
            try
            {
                var result = await client.GetAsync(requestUri);
                var resultString = await result.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(resultString) && result.IsSuccessStatusCode)
                {
                    if (typeof(TResult) == typeof(bool))
                    {
                        return JsonConvert.DeserializeObject<TResult>("true");
                    }
                }

                return JsonConvert.DeserializeObject<TResult>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return LogError<TResult>(logger, e, new object[] {requestUri});
            }
        }

        public static async Task<TResult> PostAsync<TRequest, TResult>(this HttpClient client, string requestUri,
            TRequest request, ILogger logger)
        {
            try
            {
                var result = await client.PostAsync(requestUri, GetJsonData(request));
                var resultString = await result.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(resultString) && result.IsSuccessStatusCode)
                {
                    if (typeof(TResult) == typeof(bool))
                    {
                        return JsonConvert.DeserializeObject<TResult>("true");
                    }
                }

                return JsonConvert.DeserializeObject<TResult>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                var requestType = request.GetType();
                var properties = requestType.GetProperties();
                var objectArray = properties.Select(p => p.GetValue(request)).ToArray();

                return LogError<TResult>(logger, e, objectArray);
            }
        }

        public static async Task<TResult> PutAsync<TRequest, TResult>(this HttpClient client, string requestUri,
            TRequest request, ILogger logger)
        {
            try
            {
                var result = await client.PutAsync(requestUri, GetJsonData(request));
                var resultString = await result.Content.ReadAsStringAsync();


                if (string.IsNullOrWhiteSpace(resultString) && result.IsSuccessStatusCode)
                {
                    if (typeof(TResult) == typeof(bool))
                    {
                        return JsonConvert.DeserializeObject<TResult>("true");
                    }
                }

                return JsonConvert.DeserializeObject<TResult>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                var requestType = request.GetType();
                var properties = requestType.GetProperties();
                var objectArray = properties.Select(p => p.GetValue(request)).ToArray();

                return LogError<TResult>(logger, e, objectArray);
            }
        }

        public static async Task<TResult> DeleteAsync<TResult>(this HttpClient client, string requestUri, ILogger logger)
        {
            try
            {
                var result = await client.DeleteAsync(requestUri);
                var resultString = await result.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(resultString) && result.IsSuccessStatusCode)
                {
                    if (typeof(TResult) == typeof(bool))
                    {
                        return JsonConvert.DeserializeObject<TResult>("true");
                    }
                }

                return JsonConvert.DeserializeObject<TResult>(await result.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                return LogError<TResult>(logger, e, new object[] { requestUri });
            }
        }

    }
}