using static MediCue.Models.UIModels.ChatBotOutputDTO;

namespace MediCue.Models.WebModels
{
    public class ChatBotOutputResponse
    {
        public string? role { get; set; }
        public string? content { get; set; }
    }
}
