using System.Linq;
using ZXing.Net.Maui;

namespace MediCue.Views;

public partial class ScanQRToRefillMedicine : ContentPage
{
	private DateTime _lastScanUtc = DateTime.MinValue;
	private string? _lastValue;

	public ScanQRToRefillMedicine()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
		if (status != PermissionStatus.Granted)
		{
			status = await Permissions.RequestAsync<Permissions.Camera>();
		}

		if (status != PermissionStatus.Granted)
		{
			await DisplayAlert("Error", "Camera permission is required to scan QR/Barcodes.", "OK");
		}
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
    private void OnBarcodesDetected(object? sender, BarcodeDetectionEventArgs e)
	{
		var value = e.Results?.FirstOrDefault()?.Value;
		if (string.IsNullOrWhiteSpace(value))
			return;

		var now = DateTime.UtcNow;
		if (string.Equals(value, _lastValue, StringComparison.Ordinal) && now - _lastScanUtc < TimeSpan.FromSeconds(1))
			return;

		_lastValue = value;
		_lastScanUtc = now;

		MainThread.BeginInvokeOnMainThread(() =>
		{
			ScannedTextLabel.Text = value;
		});
	}
}