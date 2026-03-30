namespace MediCue.Services.Navigation
{
    public class MauiNavigationService : INavigationService
    {
        public Dictionary<string, string>? NavigationDict { get; set; }

        public Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null)
        {
            var shellNavigation = new ShellNavigationState(route);

            return routeParameters is null
                ? Shell.Current.GoToAsync(shellNavigation)
                : Shell.Current.GoToAsync(shellNavigation, routeParameters);
        }

        public Task PopAsync() => Shell.Current.GoToAsync("..");

        public Task NavigateWithNotificationData()
        {
            throw new NotImplementedException();
        }
    }
}
