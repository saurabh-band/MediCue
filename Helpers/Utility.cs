namespace MediCue.Helpers
{
    using Microsoft.Maui.ApplicationModel.Communication;

    public static class Utility
    {
        public static async Task OpendDefaultPhoneDialer(string phoneNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(phoneNumber))
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Phone number is empty.", "OK");
                    return;
                }

                if (!PhoneDialer.IsSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Dialer is not supported on this device.", "OK");
                    return;
                }

                PhoneDialer.Open(phoneNumber);
            }
            catch (Exception ex) when (
                ex is FeatureNotSupportedException ||
                ex is ArgumentNullException ||
                ex is InvalidOperationException)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}

