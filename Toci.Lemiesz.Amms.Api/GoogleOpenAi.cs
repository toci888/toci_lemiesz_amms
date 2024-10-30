using Google.Cloud.Speech.V1;
using System;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
//using OpenAI_API;
using OpenAI.Chat;

namespace SpeechToTextChatGPT
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string audioFilePath = "path_to_your_audio_file.wav"; // Update with your audio file path
            string dbPath = "speech_texts.db";

            CreateDatabase(dbPath);
            string transcribedText = await TranscribeAudioAsync(audioFilePath);
            InsertText(dbPath, transcribedText);
            string chatGptResponse = await GetChatGptResponse(transcribedText);

            Console.WriteLine("Transcribed Text: " + transcribedText);
            Console.WriteLine("ChatGPT Response: " + chatGptResponse);
        }

        static void CreateDatabase(string dbPath)
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();
                    string createTableQuery = "CREATE TABLE texts (id INTEGER PRIMARY KEY, text TEXT)";
                    using (var command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
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

        static void InsertText(string dbPath, string text)
        {
            using (var connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                connection.Open();
                string insertQuery = "INSERT INTO texts (text) VALUES (@text)";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@text", text);
                    command.ExecuteNonQuery();
                }
            }
        }

        static async Task<string> GetChatGptResponse(string prompt)
        {
            //var openAiClient = new OpenAIAPI("your_openai_api_key"); // Replace with your OpenAI API key
            //var chatResponse = await openAiClient.Chat.CreateMessageAsync(new ChatMessage("user", prompt));
            //return chatResponse.Content;

            return null;
        }
    }
}