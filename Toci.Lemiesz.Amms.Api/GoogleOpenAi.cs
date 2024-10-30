using Google.Cloud.Speech.V1;
using System;
using System.Data;
using Npgsql;
using System.Threading.Tasks;
//using OpenAI_API;
using OpenAI.Chat;
using OpenAI.API;
using OpenAI.API.Completions;
using OpenAI.API.Models;

namespace SpeechToTextChatGPT
{
    public class ProgramListen
    {
        public static async Task Main(string[] args)
        {
            string audioFilePath = "path_to_your_audio_file.wav"; // Update with your audio file path
            string connectionString = "Host=localhost;Username=your_username;Password=your_password;Database=speech_texts"; // Update with your PostgreSQL credentials

            CreateDatabase(connectionString);
            string transcribedText = await TranscribeAudioAsync(audioFilePath);
            InsertText(connectionString, transcribedText);
            string chatGptResponse = await GetChatGptResponse(transcribedText);

            Console.WriteLine("Transcribed Text: " + transcribedText);
            Console.WriteLine("ChatGPT Response: " + chatGptResponse);
        }

        static void CreateDatabase(string connectionString)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = "CREATE TABLE IF NOT EXISTS texts (id SERIAL PRIMARY KEY, text TEXT)";
                using (var command = new NpgsqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        static async Task<string> TranscribeAudioAsync(string audioFilePath)
        {
            var speechClient = SpeechClient.Create();
            var response = await speechClient.RecognizeAsync(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                SampleRateHertz = 16000,
                LanguageCode = "en-US"
            }, RecognitionAudio.FromFile(audioFilePath));

            return string.Join(" ", response.Results.Select(result => result.Alternatives.First().Transcript));
        }

        static void InsertText(string connectionString, string text)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO texts (text) VALUES (@text)";
                using (var command = new NpgsqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@text", text);
                    command.ExecuteNonQuery();
                }
            }
        }

        static async Task<string> GetChatGptResponse(string prompt)
        {
            var openAiClient = new OpenAIAPI("your_openai_api_key"); // Replace with your OpenAI API key
            //var chatResponse = await openAiClient.Completions.StreamCompletionAsync(new CompletionRequest(prompt,new Model("")));

            //return chatResponse;

            return null;
        }
    }
}