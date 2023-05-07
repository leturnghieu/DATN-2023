using DATN.DTOs;

namespace DATN.Services.Interfaces
{
    public interface IChatGPT
    {
        Task<ChatGPTResponse> Ask(string? input);
    }
}
