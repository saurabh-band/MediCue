namespace MediCue.Services.ChatBot
{
    public class ChatBotService : IChatBotServices
    {
        private readonly HttpClient _httpClient;
        private readonly IRequestProvider _requestProvider;
        private readonly IUserPermissionsService _userPermissionsService;
        private readonly IWebMapper<ChatBotOutputResponse, ChatBotOutputDTO> _chatBotOutputWebMapper;
        private readonly IWebMapper<List<ChatBotRequestResponse>, List<ChatBotRequestDTO>> _chatBotRequestWebMapper;

        public ChatBotService(HttpClient httpClient,
                              IRequestProvider requestProvider,
                              IUserPermissionsService userPermissionsService,
                              IWebMapper<ChatBotOutputResponse, ChatBotOutputDTO> chatBotOutputWebMapper,
                              IWebMapper<List<ChatBotRequestResponse>, List<ChatBotRequestDTO>> chatBotRequestWebMapper
                              )
        {
            _httpClient = httpClient;
            _requestProvider = requestProvider;
            _userPermissionsService = userPermissionsService;

            _chatBotOutputWebMapper = chatBotOutputWebMapper;
            _chatBotRequestWebMapper = chatBotRequestWebMapper;
        }

        public async Task<ChatBotOutputDTO> GetChatBot(ChatBotRequestDTO messages)
        {
            var endpoint = APIConstants.CHAT_BOT_ENDPOINT;

            var requestBody = new ChatBotRequestDTO
            {
                Messages = messages.Messages?
                    .Where(m => m is not null)
                    .Select(m => new ChatBotRequestDTO.ChatBotMessage
                    {
                        role = m.role ?? string.Empty,
                        content = m.content ?? string.Empty
                    })
                    .ToList() ?? []
            };

            var response = await _requestProvider.PostAsync<ChatBotOutputResponse>(endpoint, requestBody);

            return response.Success && response.Data is not null
                ? _chatBotOutputWebMapper.MapToDTO(response.Data)
                : new ChatBotOutputDTO();
        }
    }
}
