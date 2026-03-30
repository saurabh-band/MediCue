namespace MediCue.Services.Mapper
{
    [Mapper]
    public partial class RecentCommunicationWebMapper : IWebMapper<RecentCommunicationResponse, RecentCommunicationDTO>
    {
        public partial RecentCommunicationDTO MapToDTO(RecentCommunicationResponse communicationResponse);

        public partial RecentCommunicationResponse MapToWebModel(RecentCommunicationDTO communicationDTO);
    }

    [Mapper]
    public partial class ChatBotRequestWebMapper : IWebMapper<List<ChatBotRequestResponse>, List<ChatBotRequestDTO>>
    {
        public partial List<ChatBotRequestDTO> MapToDTO(List<ChatBotRequestResponse> chatBotRequestResponse);

        public partial List<ChatBotRequestResponse> MapToWebModel(List<ChatBotRequestDTO> chatBotRequestDTO);
    }

    [Mapper]
    public partial class ChatBotOutputWebMapper : IWebMapper<ChatBotOutputResponse, ChatBotOutputDTO>
    {
        public partial ChatBotOutputDTO MapToDTO(ChatBotOutputResponse chatBotOutputResponse);

        public partial ChatBotOutputResponse MapToWebModel(ChatBotOutputDTO chatBotOutputDTO);
    }
}
