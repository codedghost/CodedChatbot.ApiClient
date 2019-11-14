using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
    }
}