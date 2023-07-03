using System.Net.Http.Json;

namespace napravo_schedule.API
{
    public sealed class APIWorker
    {
        string url = "https://localhost:7028/api/";
        HttpClient _client { get; set; }
        public APIWorker()
        {
            _client = new();
        }

        public async Task<T> Get<T>(string method, string parameter)
        {
            HttpResponseMessage result = await _client.GetAsync(method + '/' + parameter);
            if (result.IsSuccessStatusCode)
                return await result.Content.ReadFromJsonAsync<T>();
            else
                throw new HttpRequestException($"Code {result.StatusCode}: {await result.Content.ReadAsStringAsync()}");
        }
    }
}
