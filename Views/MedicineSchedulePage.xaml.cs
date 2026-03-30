namespace MediCue.Views;

public partial class MedicineSchedulePage : ContentPage
{
	public MedicineSchedulePage(MedicineScheduleViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
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

    private async void OnViewSummaryTapped(object sender, EventArgs e)
    {
        if (sender is Border borders)
        {
            borders.Shadow = new Shadow { Brush = Colors.Transparent };
            borders.BackgroundColor = CommonResources.GetResourceColor("buttonPressedBackgroundColor");

            await Task.Delay(200);

            borders.Shadow = new Shadow { Brush = CommonResources.GetResourceColor("shadowColor"), Opacity = (float)0.15 };
            borders.Shadow.Offset = new Point(0, 6);
            borders.BackgroundColor = CommonResources.GetResourceColor("buttonPressedBackgroundColor");
            borders.Shadow.Radius = 2;
        }
    }
}