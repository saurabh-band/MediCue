namespace MediCue.Views;

public partial class ReportsAndSummaryPage : ContentPage
{
	public ReportsAndSummaryPage(ReportsAndSummaryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ReportsAndSummaryViewModel vm)
        {
            if (vm.AppearingCommand.CanExecute(null))
                vm.AppearingCommand.Execute(null);
        }
    }

    protected override bool OnBackButtonPressed()
    {
        // Handle the hardware/system back button (Android).
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync("//Main");
        });

        // true = we handled it; don't let Shell do default back navigation.
        return true;
    }
}