using Google.Cloud.Speech.V1;
using System.Threading.Tasks;

public class SpeechToTextService
{
    private readonly SpeechClient _speechClient;

    public SpeechToTextService(SpeechClient speechClient)
    {
        _speechClient = speechClient;
    }

    public async Task<string> TranscribeAudioAsync(byte[] audioContent)
    {
        var response = await _speechClient.RecognizeAsync(new RecognitionConfig
        {
            Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
            SampleRateHertz = 16000,
            LanguageCode = "en-US",
        }, RecognitionAudio.FromBytes(audioContent));

        return response.Results
            .SelectMany(result => result.Alternatives)
            .FirstOrDefault()?.Transcript;
    }
}