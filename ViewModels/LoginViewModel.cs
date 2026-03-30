using MediCue.ViewModels.Base;

namespace MediCue.ViewModels
{
    public partial class LoginViewModel : BaseViewMoedl
    {
        #region Fields
        private readonly INavigationService _navigationService;
        #endregion

        #region ObservaleProperties
        //Add Observable Properties here
        #endregion

        #region Constructor

        public LoginViewModel(INavigationService navigationService) :base(navigationService)
        {
            _navigationService = navigationService;
        }
        #endregion

        #region RelayCommand

        [RelayCommand]
        public async Task Login()
        {
            await _navigationService.NavigateToAsync(ShellPageRoutes.HOME_PAGE);
        }
        #endregion


    }
}
