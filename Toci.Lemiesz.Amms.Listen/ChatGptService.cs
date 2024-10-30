using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

public class ChatGptService
{
    private readonly HttpClient _httpClient;

    public ChatGptService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetChatGptResponseAsync(string message)
    {
        var payload = new { prompt = message, max_tokens = 100 };
        var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/completions", payload);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Error communicating with ChatGPT API");
        }

        var result = await response.Content.ReadFromJsonAsync<JsonDocument>();
        return result?.RootElement.GetProperty("choices")[0].GetProperty("text").GetString();
    }
}
