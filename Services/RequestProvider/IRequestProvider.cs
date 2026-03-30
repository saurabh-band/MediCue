namespace MediCue.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<Response<T>> GetAsync<T>(string endpoint, string token = "", Dictionary<string, string>? headers = null);
        Task<Response<T>> PostAsync<T>(string endpoint, object data, string token = "", Dictionary<string, string>? headers = null, string? contentType = RequestProviderKeys.APPLICATION_JSON);
        Task<Response<T>> PostAsync<T>(string endpoint, string token = "", Dictionary<string, string>? headers = null, string? contentType = RequestProviderKeys.APPLICATION_JSON);
        Task<Response<T>> PutAsync<T>(string endpoint, object data, string token = "", Dictionary<string, string>? headers = null, string? contentType = RequestProviderKeys.APPLICATION_JSON);
        Task<Response<T>> DeleteAsync<T>(string endpoint, string token = "", Dictionary<string, string>? headers = null);
    }
}
