using System.Linq;
using System.Collections.ObjectModel;

namespace MediCue.ViewModels
{
    public partial class UpdateMedicineScheduleViewModel : BaseViewMoedl
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<MedicineMealRow> medicineNames = new();

        [ObservableProperty]
        private MedicineMealRow? selectedMedicineName;

        [ObservableProperty]
        private bool isMorningSelected = false;

        [ObservableProperty]
        private bool isAfternoonSelected = false;

        [ObservableProperty]
        private bool isEveningSelected = false;

        [ObservableProperty]
        private bool isBeforeMealSelected = false;

        [ObservableProperty]
        private bool isAfterMealSelected = false;
        public UpdateMedicineScheduleViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;

            LoadData();
        }

        [RelayCommand]
        private void ToggleDose(string dose)
        {
            switch (dose)
            {
                case "Morning":
                    IsMorningSelected = !IsMorningSelected;
                    break;
                case "Afternoon":
                    IsAfternoonSelected = !IsAfternoonSelected;
                    break;
                case "Evening":
                    IsEveningSelected = !IsEveningSelected;
                    break;
            }
        }

        [RelayCommand]
        private void SelectMealTiming(string timing)
        {
            IsBeforeMealSelected = string.Equals(timing, "Before Meal", StringComparison.OrdinalIgnoreCase);
            IsAfterMealSelected = string.Equals(timing, "After Meal", StringComparison.OrdinalIgnoreCase);
        }

        public void LoadData()
        {
            // Load medicine names from your data source
            MedicineNames.Add(new MedicineMealRow
            {
                Medicine = "Glycomet 500",
                Dosage = new List<string> { "Morning", "Afternoon", "Evening" },
                MealTiming = "After Meal"
            });
            MedicineNames.Add(new MedicineMealRow
            {
                Medicine = "Vitagreat",
                Dosage = new List<string> { "Morning", "Evening" },
                MealTiming =  "Before Meal"
            });
            MedicineNames.Add(new MedicineMealRow
            {
                Medicine = "Lobate 100",
                Dosage = new List<string> { "Morning", "Afternoon", "Evening" },
                MealTiming = "After Meal"
            });
            MedicineNames.Add(new MedicineMealRow
            {
                Medicine = "Rabekind 20",
                Dosage = new List<string> { "Morning", "Afternoon", "Evening" },
                MealTiming = "Before Meal"
            });
            MedicineNames.Add(new MedicineMealRow
            {
                Medicine = "Benidine 4",
                Dosage = new List<string> { "Evening" },
                MealTiming = "Before Meal"
            });
        }

        partial void OnSelectedMedicineNameChanged(MedicineMealRow? value)
        {
            if (value is null)
                return;

            var dosage = value.Dosage ?? new List<string>();

            IsMorningSelected = dosage.Any(d => string.Equals(d, "Morning", StringComparison.OrdinalIgnoreCase));
            IsAfternoonSelected = dosage.Any(d => string.Equals(d, "Afternoon", StringComparison.OrdinalIgnoreCase));
            IsEveningSelected = dosage.Any(d => string.Equals(d, "Evening", StringComparison.OrdinalIgnoreCase));

            IsBeforeMealSelected = string.Equals(value.MealTiming, "Before Meal", StringComparison.OrdinalIgnoreCase);
            IsAfterMealSelected = string.Equals(value.MealTiming, "After Meal", StringComparison.OrdinalIgnoreCase);

            OnPropertyChanged("IsMorningSelected");
            OnPropertyChanged("IsAfternoonSelected");
            OnPropertyChanged("IsEveningSelected");
            OnPropertyChanged("IsBeforeMealSelected");
            OnPropertyChanged("IsAfterMealSelected");
        }
    }
}
