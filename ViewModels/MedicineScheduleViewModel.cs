namespace MediCue.ViewModels
{
    public partial class MedicineScheduleViewModel : BaseViewMoedl
    {
        private readonly INavigationService _navigationService;


        public MedicineScheduleViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        private async Task NavigateToAddNewMedicineSchedulePage()
        {
            await _navigationService.NavigateToAsync(nameof(AddNewMedicineSchedulePage));
        }

        [RelayCommand]
        private async Task NavigateToUpdateMedicineSchedulePage()
        {
            await _navigationService.NavigateToAsync(nameof(UpdateMedicineSchedulePage));
        }
    }
}
