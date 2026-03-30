namespace MediCue.Views;

public partial class AppSettingsPage : ContentPage
{
	public AppSettingsPage()
	{
		InitializeComponent();
	}

    protected override bool OnBackButtonPressed()
    {
        // Handle the hardware/system back button (Android).
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync("//Home");
        });

        // true = we handled it; don't let Shell do default back navigation.
        return true;
    }
}