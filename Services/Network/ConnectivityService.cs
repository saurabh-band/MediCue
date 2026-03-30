using MediCue.Resources.Strings;
using System.Threading.Tasks;

namespace MediCue.Services.Network
{
    public class ConnectivityService : IConnectivityService, IDisposable
    {
        private bool _isConnected;
        private bool _disposed;

        public ConnectivityService()
        {
            _isConnected = Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

            Connectivity.Current.ConnectivityChanged += OnConnectivityChanged;
        }

        private async void OnConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            bool newConnectionStatus = e.NetworkAccess == NetworkAccess.Internet;

            if (_isConnected != newConnectionStatus)
            {
                _isConnected = newConnectionStatus;

                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await ShowConnectivityMessage(_isConnected);
                });
            }
        }

        private static async Task ShowConnectivityMessage(bool isConnected)
        {
            if (!isConnected)
                await App.Current.MainPage.DisplayAlert(AppResources.ConnectivityStatus, AppResources.ConnectivityStatusMessage, "OK");
        }

        public async Task<bool> CheckConnectivityAsync()
        {
            _isConnected = Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

            await ShowConnectivityMessage(_isConnected);

            return _isConnected;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Connectivity.Current.ConnectivityChanged -= OnConnectivityChanged;
                }
                _disposed = true;
            }
        }
    }
}
