using System.Net.Http.Json;
using RecruitmentTask.Model;

namespace RecruitmentTask.Client
{
    public class ApiClient(HttpClient httpClient):IApiClient
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<CatFact> GetDataAsync(
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("", cancellationToken);

            response.EnsureSuccessStatusCode();

            CatFact? catFact = await response.Content.ReadFromJsonAsync<CatFact>(cancellationToken);

            return catFact is null ? 
                throw new InvalidOperationException("API returned an empty response.") : catFact;
        }
    }
}
