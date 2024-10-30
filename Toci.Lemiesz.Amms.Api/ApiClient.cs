using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendConversationEntryAsync(CreateConversationEntryDto entryDto)
    {
        var content = new StringContent(JsonSerializer.Serialize(entryDto), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/api/ConversationEntries", content);

        response.EnsureSuccessStatusCode();
    }
}