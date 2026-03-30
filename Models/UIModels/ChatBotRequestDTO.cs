namespace MediCue.Models.UIModels
{
    public class ChatBotRequestDTO
    {
        public List<ChatBotMessage>? Messages { get; set; }

        public class ChatBotMessage
        {
            public string? role { get; set; }
            public string? content { get; set; }
        }
    }
}
