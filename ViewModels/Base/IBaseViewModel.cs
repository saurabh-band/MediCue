namespace MediCue.ViewModels.Base
{
    public interface IBaseViewModel
    {
        public INavigationService NavigationService { get; }

        public IAsyncRelayCommand InitializeAsyncCommand { get; }

        public bool IsBusy { get; set; }

        public bool IsInitialized { get; set; }

        Task InitializeAsync();
    }
}
