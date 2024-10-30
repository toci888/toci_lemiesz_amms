using Google.Cloud.Speech.V1;
using System.Threading.Tasks;

public class SpeechToTextService
{
    private readonly SpeechClient _speechClient;

    public SpeechToTextService()
    {
        _speechClient = SpeechClient.Create();
    }

    public async Task<string> RecognizeSpeechAsync(byte[] audioData)
    {
        var response = await _speechClient.RecognizeAsync(new RecognitionConfig
        {
            Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
            SampleRateHertz = 16000,
            LanguageCode = "pl-PL"
            
        }, RecognitionAudio.FromBytes(audioData));

        return response.Results
            .Select(result => result.Alternatives.FirstOrDefault()?.Transcript)
            .FirstOrDefault() ?? string.Empty;
    }
}