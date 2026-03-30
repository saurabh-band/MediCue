namespace MediCue.Services.Navigation
{
    public interface INavigationService
    {
        Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null);
        Task PopAsync();
        Dictionary<string, string>? NavigationDict { get; set; }
        Task NavigateWithNotificationData();
    }
}
