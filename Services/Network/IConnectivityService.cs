namespace MediCue.Services.Network
{
    public interface IConnectivityService
    {
        Task<bool> CheckConnectivityAsync();
    }
}
