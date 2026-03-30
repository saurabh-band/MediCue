using Syncfusion.Maui.Chat;

namespace MediCue.ViewModels
{
    public partial class ChatBotViewModel : BaseViewMoedl
    {
        INavigationService _navigationService;
        private readonly IChatBotServices _chatBotServices;

        [ObservableProperty]
        public Author currentUser;

        [ObservableProperty]
        public ObservableCollection<object> messages;

        [ObservableProperty]
        public Author botUser = new Author() { Name = "MediCue", Avatar = "chatassitance.png" };

        public ChatBotViewModel(INavigationService navigationService, IChatBotServices chatBotServices) : base(navigationService)
        {
            _navigationService = navigationService;
            _chatBotServices = chatBotServices;

            Messages = new ObservableCollection<object>();
            CurrentUser = new Author() { Name = "Friend", Avatar = "user.png" };

        }

        [RelayCommand]
        public async Task SendMessage(Syncfusion.Maui.Chat.SendMessageEventArgs? parameter)
        {

            if (parameter?.Message is null)
            {
                return;
            }

            var userMessage = parameter.Message.Text ?? String.Empty;

            var repsonse = await _chatBotServices.GetChatBot(new ChatBotRequestDTO
            {
                Messages = new List<ChatBotRequestDTO.ChatBotMessage>
                {
                    new ChatBotRequestDTO.ChatBotMessage
                    {
                        role = "user",
                        content = userMessage
                    }
                }
            });

            if (!string.IsNullOrWhiteSpace(repsonse?.content))
            {
                Messages.Add(new Syncfusion.Maui.Chat.TextMessage
                {
                    Author = BotUser,
                    Text = repsonse.content,
                    DateTime = DateTime.Now
                });
            }
        }
    }
}
