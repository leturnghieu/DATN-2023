using System.Text.Json.Serialization;

namespace DATN.DTOs
{
    public class OpenAIDTOs
    {
        public class OpenAIRequest
        {
            public string? model { get; set; }
            public List<Message>? messages { get; set; }
        }
        public class Message
        {
            public string? role { get; set; }
            public string? content { get; set; }
        }
    }
}
