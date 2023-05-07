using DATN.DTOs;
using DATN.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static DATN.DTOs.OpenAIDTOs;

namespace DATN.Services.Service
{
    public class ChatGPTService : IChatGPT
    {
        public async Task<ChatGPTResponse> Ask(string? input)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");
            var request = new OpenAIRequest
            {
                model = "gpt-3.5-turbo",
                messages = new List<Message>
                {
                    new Message
                    {
                        role = "user",
                        content = input.Trim()
                    }
                }
            };
            var json = System.Text.Json.JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var resjson = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<dynamic>(resjson);
            //var data = JsonSerializer.Deserialize(resjson, typeof(object));
            return new ChatGPTResponse
            {
                Result = data?.choices[0].message.content
            };
        }
    }
}
