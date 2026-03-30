namespace MediCue.ViewModels.Base
{
    public abstract partial class BaseViewMoedl : ObservableObject, IQueryAttributable ,IBaseViewModel
    {
        private long _isBusy;

        public bool IsBusy
        {
            get => Interlocked.Read(ref _isBusy) == 1;
            set => Interlocked.Exchange(ref _isBusy, value ? 1 : 0);
        }

        [ObservableProperty]
        private bool _isInitialized;

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private bool _isRefeshView = false;

        public INavigationService NavigationService { get; }
        public IAsyncRelayCommand InitializeAsyncCommand { get; }

        public BaseViewMoedl(INavigationService navigationService)
        {
            NavigationService = navigationService;
            InitializeAsyncCommand = new AsyncRelayCommand(
                async () =>
                {
                    await IsBusyFor(InitializeAsync);
                    IsInitialized = true;
                },
                AsyncRelayCommandOptions.FlowExceptionsToTaskScheduler);
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
            => ApplyQueryAttributes(query);

        public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            // Override in derived classes to handle query parameters
        }

        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        protected async Task IsBusyFor(Func<Task> unitOfWork)
        {
            Interlocked.Increment(ref _isBusy);
            OnPropertyChanged(nameof(IsBusy));

            try
            {
                await unitOfWork();
            }
            finally
            {
                Interlocked.Decrement(ref _isBusy);
                OnPropertyChanged(nameof(IsBusy));
            }
        }
    }
}
