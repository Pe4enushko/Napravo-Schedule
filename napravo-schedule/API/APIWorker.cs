using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace napravo_schedule.API
{
    internal class APIWorker<T> : IDisposable
    {
        static JsonSerializerOptions jsonOptions;

        static string _testAccessToken = "";
        static string _baseUrl = "https://localhost:7028";
        HttpClient _httpClient;

        public APIWorker()
        {
            if (jsonOptions == null)
                jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = true };
            _httpClient = new();
        }
        public APIWorker(int timeout) : this()
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(timeout);
        }

        public void Dispose()
        {
            _httpClient.CancelPendingRequests();
            _httpClient.Dispose();
            this.Dispose();
        }

        /// <summary>
        /// Performs get request with the base url of "https://api-messenger.florgon.com/v1"
        /// </summary>
        /// <exception cref="HttpRequestException">API didn't respond</exception>
        /// <exception cref="InvalidOperationException">API has thrown ValidationError</exception>
        /// <returns></returns>
        internal async Task<T> GetAsync(APIRequest requestData)
        {
            // Add access token to any request by default (no tokens atm)
            // requestData.args.Add("access_token", _testAccessToken);

            StringBuilder stringBuilder = new StringBuilder(_baseUrl);
            stringBuilder.Append(requestData.urlMethod);
            bool firstArg = true;
            foreach (var arg in requestData.args)
            {
                stringBuilder.Append((firstArg ? "?" : "&")
                    + $"{arg.Key}={arg.Value}");

                firstArg = false;
            }

            int attempts = 1;

            Requesting:
            try
            {
                var response = await _httpClient.GetAsync(stringBuilder.ToString());
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new HttpRequestException($"Error! Http response code: {response.StatusCode}");

                return await JsonSerializer.DeserializeAsync<T>(new MemoryStream(Encoding.UTF8.GetBytes(json)), jsonOptions);
            }
            catch (HttpRequestException)
            {
                if (attempts < 6)
                {
                    attempts++;
                    await Task.Delay(200 * attempts);
                    goto Requesting;
                }
                else
                    throw new HttpRequestException("Http requests have failed");
            }

        }
    }
}
