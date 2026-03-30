namespace MediCue.Views;

public partial class EmergencyContactPage : ContentPage
{
	public EmergencyContactPage()
	{
		InitializeComponent();
	}

    private async void OnBackClicked(object sender, EventArgs e)
    {
        // Navigate to Home tab/root
        await Shell.Current.GoToAsync("//Home");
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