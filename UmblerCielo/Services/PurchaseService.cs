using System.Net.Http;
using System.Threading.Tasks;

public class PurchaseService
{
    private readonly HttpClient _httpClient;

    public PurchaseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> TestBackendConnection()
    {
        return await _httpClient.GetStringAsync("api/Purchase/TestConnection");
    }
}
