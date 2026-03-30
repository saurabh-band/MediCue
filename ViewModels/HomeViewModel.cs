namespace MediCue.ViewModels
{
    public partial class HomeViewModel : BaseViewMoedl
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        string? doctorPhone = "+15551234567";

        [ObservableProperty]
        string? pharmacyPhone = "+15551234567";

        [ObservableProperty]
        string? relativePhone = "+15551234567";

        [ObservableProperty]
        DateTime? todaysDateTime = DateTime.Now;


        public HomeViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;

            LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
        }

        private async void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
        {
            var nowDateTime = DateTime.Now;
            var now = TimeOnly.FromDateTime(nowDateTime);

            var morningStart = new TimeOnly(0, 0);
            var morningEnd = new TimeOnly(9, 0);

            var afternoonStart = new TimeOnly(11, 0);
            var afternoonEnd = new TimeOnly(16, 0);

            var eveningStart = new TimeOnly(16, 0);
            var endOfDay = new TimeOnly(23, 59, 59);

            if (e.IsTapped)
            {
                if (now >= morningStart && now < morningEnd)
                    await MorningMedicineDetails();
                else if (now >= afternoonStart && now < afternoonEnd)
                    await AfternoonMedicineDetails();
                else if (now >= eveningStart && now <= endOfDay)
                    await EveningMedicineDetails();
            }
        }

        [RelayCommand]
        public async Task Appearing()
        {
            await Task.CompletedTask;
        }

        [RelayCommand]
        public async Task ChatBot()
        {
            await _navigationService.NavigateToAsync(nameof(ChatBotPage));
        }

        [RelayCommand]
        public async Task LocalNotification()
        {
            const string morningDescription = "It's time to take your morning medicine.";
            const string afternoonDescription = "It's time to take your afternoon medicine.";
            const string eveningDescription = "It's time to take your evening medicine.";

            var nowDateTime = DateTime.Now;
            var now = TimeOnly.FromDateTime(nowDateTime);

            var morningStart = new TimeOnly(0, 0);
            var morningEnd = new TimeOnly(9, 0);

            var afternoonStart = new TimeOnly(11, 0);
            var afternoonEnd = new TimeOnly(16, 0);

            var eveningStart = new TimeOnly(16, 0);
            var endOfDay = new TimeOnly(23, 59, 59);

            string description =
                now >= morningStart && now < morningEnd ? morningDescription :
                now >= afternoonStart && now < afternoonEnd ? afternoonDescription :
                now >= eveningStart && now <= endOfDay ? eveningDescription :
                morningDescription;

            var request = new NotificationRequest
            {
                NotificationId = 1337,
                Title = "MediCine Reminder",
                Description = description,
                BadgeNumber = 42,
                CategoryType = NotificationCategoryType.Reminder,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1),
                    NotifyRepeatInterval = TimeSpan.FromDays(1),
                    RepeatType = NotificationRepeat.Daily,
                }
            };

            await LocalNotificationCenter.Current.Show(request);

            await Task.CompletedTask;
        }

        [RelayCommand]
        public async Task PageRefresh()
        {
            await Task.CompletedTask;
        }

        [RelayCommand]
        public async Task Disappearing()
        {
            await Task.CompletedTask;
        }

        [RelayCommand]
        public async Task MorningMedicineDetails()
        {
            var page = Application.Current?.MainPage;

            if (page == null)
                return;

            var rows = new List<MedicineMealRow>
            {
                new() { Medicine = "Glycomet 500",     MealTiming = "After Meal" },
                new() { Medicine = "Vitagreat",   MealTiming = "Before Meal" },
                new() { Medicine = "Lobate 100", MealTiming = "After Meal" },
                new() { Medicine = "Rabekind 20", MealTiming = "Before Meal" },
            };

            await page.ShowPopupAsync(new MedicineMealTablePopup("Morning Medicines", rows));

        }

        [RelayCommand]
        public async Task AfternoonMedicineDetails()
        {
            var page = Application.Current?.MainPage;

            if (page == null)
                return;

            var rows = new List<MedicineMealRow>
            {
                new() { Medicine = "Glycomet 500",     MealTiming = "After Meal" },
                new() { Medicine = "Lobate 100", MealTiming = "After Meal" },
            };

            await page.ShowPopupAsync(new MedicineMealTablePopup("Afternoon Medicines", rows));

        }

        [RelayCommand]
        public async Task EveningMedicineDetails()
        {
            var page = Application.Current?.MainPage;

            if (page == null)
                return;

            var rows = new List<MedicineMealRow>
            {
                new() { Medicine = "Glycomet 500",     MealTiming = "After Meal" },
                new() { Medicine = "Vitagreat",   MealTiming = "Before Meal" },
                new() { Medicine = "Lobate 100", MealTiming = "After Meal" },
                new() { Medicine = "Rabekind 20", MealTiming = "Before Meal" },
                new() { Medicine = "Benidine 4", MealTiming = "Before Meal" },
            };

            await page.ShowPopupAsync(new MedicineMealTablePopup("Evening Medicines", rows));

        }

        [RelayCommand]
        public async Task ViewSummary()
        {
            await _navigationService.NavigateToAsync(nameof(ReportSummaryTextPage));
        }

        [RelayCommand]
        public async Task UploadReport()
        {
            await _navigationService.NavigateToAsync(nameof(UploadReportPage));
        }

        [RelayCommand]
        public async Task CallDoctor()
        {
            try
            {
                await Utility.OpendDefaultPhoneDialer(DoctorPhone ?? string.Empty);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calling doctor: {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task CallPharmacy()
        {
            await Utility.OpendDefaultPhoneDialer(PharmacyPhone ?? string.Empty);
        }

        [RelayCommand]
        public async Task CallRelative()
        {
            await Utility.OpendDefaultPhoneDialer(RelativePhone ?? string.Empty);
        }

        [RelayCommand]
        public async Task UpdateRoutineCheck()
        {
            await Task.CompletedTask;
        }
    }
}
