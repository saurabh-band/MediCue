namespace MediCue
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoute();

            Application.Current.RequestedThemeChanged += (s, a) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        StatusBar.StatusBarColor = CommonResources.GetResourceColor("StatusBarColor");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to set status bar color. : {ex.Message}");
                    }
                });
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
                await Permissions.RequestAsync<Permissions.Camera>();
        }

        protected override bool OnBackButtonPressed()
        {
            // If flyout is open, close it and consume back press
            if (FlyoutIsPresented)
            {
                FlyoutIsPresented = false;
                return true;
            }

            return base.OnBackButtonPressed();
        }

        private static void RegisterRoute()
        {
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            //Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            //Routing.RegisterRoute(nameof(ForgotPasswordPage), typeof(ForgotPasswordPage));
            //Routing.RegisterRoute(nameof(ResetPasswordPage), typeof(ResetPasswordPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(EmergencyContactPage), typeof(EmergencyContactPage));
            Routing.RegisterRoute(nameof(PersonalDetailsPage), typeof(PersonalDetailsPage));
            Routing.RegisterRoute(nameof(MedicineSummaryPage), typeof(MedicineSummaryPage));
            Routing.RegisterRoute(nameof(ReportsAndSummaryPage), typeof(ReportsAndSummaryPage));
            Routing.RegisterRoute(nameof(ReportSummaryTextPage), typeof(ReportSummaryTextPage));
            Routing.RegisterRoute(nameof(MedicineRoutineSummary), typeof(MedicineRoutineSummary));
            Routing.RegisterRoute(nameof(UploadReportPage), typeof(UploadReportPage));
            Routing.RegisterRoute(nameof(MedicineSchedulePage), typeof(MedicineSchedulePage));
            Routing.RegisterRoute(nameof(AddNewMedicineSchedulePage), typeof(AddNewMedicineSchedulePage));
            Routing.RegisterRoute(nameof(UpdateMedicineSchedulePage), typeof(UpdateMedicineSchedulePage));
            Routing.RegisterRoute(nameof(ChatBotPage), typeof(ChatBotPage));
            Routing.RegisterRoute(nameof(ScanQRToRefillMedicine), typeof(ScanQRToRefillMedicine));
            Routing.RegisterRoute(nameof(RefillMedicinePage), typeof(RefillMedicinePage));
            //Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(AppSettingsPage), typeof(AppSettingsPage));
            //Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        }
    }
}