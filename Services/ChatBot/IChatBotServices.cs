namespace MediCue.Services.ChatBot
{
    public interface IChatBotServices
    {
        Task<ChatBotOutputDTO> GetChatBot(ChatBotRequestDTO messages);
    }
}
