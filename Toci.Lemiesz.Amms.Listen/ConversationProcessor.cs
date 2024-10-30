using System.Threading.Tasks;

public class ConversationProcessor
{
    private readonly SpeechToTextService _speechRecognitionService;
    private readonly ChatGptService _chatGPTService;
    private readonly ApiClient _apiClient;

    public ConversationProcessor(SpeechToTextService speechRecognitionService, ChatGptService chatGPTService, ApiClient apiClient)
    {
        _speechRecognitionService = speechRecognitionService;
        _chatGPTService = chatGPTService;
        _apiClient = apiClient;
    }

    public async Task ProcessAudioAsync(byte[] audioData, int sessionId, int senderId)
    {
        // Rozpoznanie mowy
        var recognizedText = await _speechRecognitionService.RecognizeSpeechAsync(audioData);

        // Uzyskanie odpowiedzi z ChatGPT
        var chatGptResponse = await _chatGPTService.GetChatGptResponseAsync(recognizedText);

        // Przygotowanie DTO
        var entryDto = new CreateConversationEntryDto
        {
            SessionId = sessionId,
            SenderId = senderId,
            MessageText = recognizedText,
            IsVoice = true,
            ChatGPTResponse = chatGptResponse
        };

        // Wysłanie danych do API
        await _apiClient.SendConversationEntryAsync(entryDto);
    }
}