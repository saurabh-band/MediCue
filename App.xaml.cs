namespace MediCue
{
    public partial class App : Application
    {
        #region Services
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnectivityService _connectivityService;
        private readonly IUserPermissionsService _userPermissionService;
        #endregion

        #region Constructor
        public App(IServiceProvider serviceProvider, IConnectivityService connectivityService, IUserPermissionsService userPermissionService)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JGaF5cXGpCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdlWX5ceXVTQ2VcU013X0pWYEs=");

            InitializeComponent();

            UserAppTheme = AppTheme.Light;

            _serviceProvider = serviceProvider;
            _connectivityService = connectivityService;
            _userPermissionService = userPermissionService;

            //Start checking connectivity
            Task.Run(() => _connectivityService.CheckConnectivityAsync());

            SetCulture();
        }
        #endregion

        #region Override Methods
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }
        #endregion

        #region Private Methods
        private void SetCulture()
        {
            var localizationService = _serviceProvider.GetRequiredService<ILocalizationService>();
            var culture = CultureInfo.CurrentCulture;
            localizationService.SetCulture(culture);
        }
        #endregion

    }
}