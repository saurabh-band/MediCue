namespace MediCue.Services.UserActivity
{
    public interface IInActivityService
    {
        DateTime LastInputTime { get; set; }
        Task HandleAppResumedAsync();
        Task HandleAppMinimizedAsync(); 
        Task UpdateLastInputTimeAsync();
    }
}
