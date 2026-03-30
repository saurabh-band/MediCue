namespace MediCue.Services.UserPermissions
{
    public interface IUserPermissionsService
    {
        Task AddSettings();
        Task ClearSettings();
        Task<string?> GetTokenAsync();
        Task SetTokenAsync(string token);
        void RemoveTokenAsync();
    }
}
