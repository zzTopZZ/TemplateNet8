using System.Net.Http.Json;

namespace Catalog.UI.Shared.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _client;

        public ApiService(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("Catalog.Api");
        }

        public async Task<T> GetAsync<T>(string route)
        {
            var data = await _client.GetFromJsonAsync<T>(route);
            return data ?? default!;
        }

        public async Task<T> PostAsync<T>(string route, object data)
        {
            var newObj = await _client.PostAsJsonAsync(route, data);
            return await newObj.Content.ReadFromJsonAsync<T>() ?? default!;
        }

        public async Task<HttpResponseMessage> UpdateAsync(string route, object data)
        {
            var response = await _client.PutAsJsonAsync(route, data);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string route)
        {
            var response = await _client.DeleteAsync(route);
            return response;
        }
    }
}
