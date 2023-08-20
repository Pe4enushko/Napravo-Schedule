using napravo_schedule.UserMessaging;
using System.Net;
using System.Text;
using System.Text.Json;

namespace napravo_schedule.API
{
    internal class APIWorker<T> : IDisposable
    {
        static JsonSerializerOptions jsonOptions;
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

        string BuildRequestString(APIRequest requestData)
        {
            StringBuilder stringBuilder = new StringBuilder(requestData.requestUrl);

            bool firstArg = true;
            foreach (var arg in requestData.args)
            {
                stringBuilder.Append((firstArg ? "?" : "&")
                    + $"{arg.Key}={arg.Value}");

                firstArg = false;
            }
            return stringBuilder.ToString();
        }
        internal async Task<T> GetAsync(APIRequest requestData)
        {
            var req = BuildRequestString(requestData);
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync(req);
                var json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    AlertDisplayer.DisplayError(
                        $"Http response code: {response.StatusCode}\n" +
                        $" Message: {response.ReasonPhrase}");

                    return default(T);
                }
                var res = await JsonSerializer
                    .DeserializeAsync<T>(new MemoryStream(Encoding.UTF8.GetBytes(json)), jsonOptions);
                return res;
            }
            catch (WebException exc)
            {
                AlertDisplayer.DisplayError("Server problems\n Message: " + exc.Message);
                return default(T);
            }
        }
    }
}
