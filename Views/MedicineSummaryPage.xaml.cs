namespace MediCue.Views;

public partial class MedicineSummaryPage : ContentPage
{
	public MedicineSummaryPage(MedicineSummaryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
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