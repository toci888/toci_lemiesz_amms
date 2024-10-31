using Google.Cloud.Speech.V1;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Api.Controllers // Replace with your actual namespace
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeechToTextController : ControllerBase
    {
        private readonly SpeechClient _speechClient;

        public SpeechToTextController()
        {
            // Initialize the Google Cloud Speech client
            _speechClient = SpeechClient.Create();
        }

        [HttpPost]
        public async Task<ActionResult<string>> ConvertSpeechToText([FromBody] AudioRequest audioRequest)
        {
            if (audioRequest == null || string.IsNullOrEmpty(audioRequest.Audio))
            {
                return BadRequest("Audio data is required.");
            }

            try
            {
                // Decode the Base64 audio data
                var audioBytes = Convert.FromBase64String(audioRequest.Audio);

                // Configure the recognition request
                var recognitionConfig = new RecognitionConfig
                {
                    Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                    SampleRateHertz = 16000,
                    LanguageCode = "pl-PL",
                };

                var recognitionAudio = new RecognitionAudio
                {
                    Content = Google.Protobuf.ByteString.CopyFrom(audioBytes)
                };

                // Perform speech recognition
                var response = await _speechClient.RecognizeAsync(recognitionConfig, recognitionAudio);
                var transcription = string.Join("\n", response.Results.Select(result => result.Alternatives[0].Transcript));

                return Ok(transcription);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error recognizing speech: {ex.Message}");
                return StatusCode(500, "Error recognizing speech");
            }
        }
    }

    public class AudioRequest
    {
        public string Audio { get; set; } // Base64 encoded audio data
    }
}