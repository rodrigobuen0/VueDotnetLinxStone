using Azure;
using VueDotnetLinxStone.Api.Entities;

namespace VueDotnetLinxStone.Api.Services
{
    public interface IFakeStoreApiClient
    {
        Task AddProductAsync(FakeStoreProductDto productDto);
        Task UpdateProductAsync(int id, FakeStoreProductDto productDto);
        Task DeleteProductAsync(int id);
    }

    public class FakeStoreApiClient : IFakeStoreApiClient
    {
        private readonly HttpClient _httpClient;

        public FakeStoreApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task AddProductAsync(FakeStoreProductDto productDto)
        {
            var response = await _httpClient.PostAsJsonAsync("products", productDto);
            response.EnsureSuccessStatusCode();

        }

        public async Task UpdateProductAsync(int id, FakeStoreProductDto productDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"products/{id}", productDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
