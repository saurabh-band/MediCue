using System;
using System.Collections.Generic;
using System.Text;

namespace MediCue.ViewModels
{
    public partial class RefillMedicineViewModel:BaseViewMoedl
    {
        private readonly INavigationService _navigationService;
        public RefillMedicineViewModel(INavigationService navigationService):base(navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
