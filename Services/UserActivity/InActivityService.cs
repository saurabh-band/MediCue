namespace MediCue.Services.UserActivity
{
    public class InActivityService : IInActivityService
    {
        private readonly TimeSpan _timeOutDuration = TimeSpan.FromMinutes(10);
        private CancellationTokenSource? cts;
        private bool isAppActive = true;
        public DateTime LastInputTime { get; set; } = DateTime.Now;


        public async Task HandleAppResumedAsync()
        {
            isAppActive = true;
            await UpdateLastInputTimeAsync();
        }
        public async Task HandleAppMinimizedAsync()
        {
            await Task.Run(() =>
            {
                isAppActive = false;
            });
        }
        public async Task UpdateLastInputTimeAsync()
        {
            if (isAppActive)
            {
                LastInputTime = DateTime.Now;
                await RestartTimerAsync();
            }
        }

        private async Task RestartTimerAsync()
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();
            DateTime expectedTimeOut = LastInputTime.Add(_timeOutDuration);

            try
            {
                while (DateTime.Now < expectedTimeOut)
                {
                    var remainingTime = expectedTimeOut - DateTime.Now;
                    await Task.Delay(remainingTime, cts.Token);
                }
                if (!cts.Token.IsCancellationRequested)
                {
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        //Perform timeout actions here
                        //await WebUtility.Logout();
                        await Task.CompletedTask;
                    });
                }
            }
            catch (TaskCanceledException)
            {
                // Timer was canceled, no action needed
            }
        }
    }
}
