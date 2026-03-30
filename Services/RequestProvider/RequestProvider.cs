namespace MediCue.Services.RequestProvider
{
    public class RequestProvider : IRequestProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IConnectivityService _connectivityService;

        public RequestProvider(HttpClient httpClient, IConnectivityService connectivityService)
        {
            _httpClient = httpClient;
            _connectivityService = connectivityService;
        }

        private static void AddHeaders(HttpRequestMessage request, Dictionary<string, string>? headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }

        public async Task<Response<T>> GetAsync<T>(string endpoint, string token = "", Dictionary<string, string>? headers = null)
        {
            try
            {
                if (!await _connectivityService.CheckConnectivityAsync())
                    return new Response<T>(false, AppResources.ConnectivityStatusMessage);


                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

                AddHeaders(request, headers);

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _httpClient.SendAsync(request);
                await HandleHttpResponse(response);
                var json = await response.Content.ReadAsStringAsync();
                return new Response<T>(true, "Success", JsonSerializer.Deserialize<T>(json));
            }
            catch (Exception ex)
            {
                await HandleException(ex);
                return new Response<T>(false, ex.Message);
            }
        }

        public async Task<Response<T>> PostAsync<T>(string endpoint, object data, string token = "", Dictionary<string, string>? headers = null, string? contentType = "application/json")
        {
            try
            {
                if (!await _connectivityService.CheckConnectivityAsync())
                    return new Response<T>(false, AppResources.ConnectivityStatusMessage);

                if (string.IsNullOrWhiteSpace(endpoint))
                    throw new ArgumentException("Endpoint is required.", nameof(endpoint));

                if (!Uri.TryCreate(endpoint, UriKind.Absolute, out _) && _httpClient.BaseAddress is null)
                    throw new InvalidOperationException();


                var content = CreateHttpContent(data, contentType);
                var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
                {
                    Content = content
                };

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                AddHeaders(request, headers);
                var response = await _httpClient.SendAsync(request);
                await HandleHttpResponse(response);
                var json = await response.Content.ReadAsStringAsync();
                return String.IsNullOrEmpty(json) 
                    ? new Response<T>(true, "Success") 
                    : new Response<T>(true, "Success", JsonSerializer.Deserialize<T>(json));

            }
            catch (Exception ex)
            {
                await HandleException(ex);
                return new Response<T>(false, ex.Message);
            }
        }

        public async Task<Response<T>> PostAsync<T>(string endpoint, string token = "", Dictionary<string, string>? headers = null, string? contentType = "application/json")
        {
            try
            {
                if (!await _connectivityService.CheckConnectivityAsync())
                    return new Response<T>(false, AppResources.ConnectivityStatusMessage);

                var request = new HttpRequestMessage(HttpMethod.Post, endpoint);


                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                AddHeaders(request, headers);
                var response = await _httpClient.SendAsync(request);
                await HandleHttpResponse(response);
                var json = await response.Content.ReadAsStringAsync();
                return new Response<T>(true, "Success", JsonSerializer.Deserialize<T>(json));

            }
            catch (Exception ex)
            {
                await HandleException(ex);
                return new Response<T>(false, ex.Message);
            }
        }



        public async Task<Response<T>> PutAsync<T>(string endpoint, object data, string token = "", Dictionary<string, string>? headers = null, string? contentType = "application/json")
        {
            try
            {
                if (!await _connectivityService.CheckConnectivityAsync())
                    return new Response<T>(false, AppResources.ConnectivityStatusMessage);

                var content = CreateHttpContent(data, contentType);
                var request = new HttpRequestMessage(HttpMethod.Put, endpoint)
                {
                    Content = content
                };

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                AddHeaders(request, headers);
                var response = await _httpClient.SendAsync(request);
                await HandleHttpResponse(response);
                var json = await response.Content.ReadAsStringAsync();
                return new Response<T>(true, "Success", JsonSerializer.Deserialize<T>(json));

            }
            catch (Exception ex)
            {
                await HandleException(ex);
                return new Response<T>(false, ex.Message);
            }
        }

        public async Task<Response<T>> DeleteAsync<T>(string endpoint, string token = "", Dictionary<string, string>? headers = null)
        {
            try
            {
                if (!await _connectivityService.CheckConnectivityAsync())
                    return new Response<T>(false, AppResources.ConnectivityStatusMessage);


                var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);

                AddHeaders(request, headers);

                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _httpClient.SendAsync(request);
                await HandleHttpResponse(response);
                var json = await response.Content.ReadAsStringAsync();
                return String.IsNullOrEmpty(json) 
                    ? new Response<T>(true, "Success") 
                    : new Response<T>(true, "Success", JsonSerializer.Deserialize<T>(json));
            }
            catch (Exception ex)
            {
                await HandleException(ex);
                return new Response<T>(false, ex.Message);
            }
        }
        private static HttpContent CreateHttpContent(object data, string? contentType)
        {
            if (data != null)
            {
                if (!String.IsNullOrEmpty(contentType))
                {

                    if (contentType == "application/json")
                    {
                        var json = JsonSerializer.Serialize(data);
                        return new StringContent(json, Encoding.UTF8, contentType);
                    }
                    else if (contentType == "application/x-www-form-urlencoded")
                    { 
                        var keyValueContent = data as IEnumerable<KeyValuePair<string, string>> ?? throw new ArgumentNullException(nameof(data), "Invalid Data Parameters");
                        return new FormUrlEncodedContent(keyValueContent);
                    }
                    else if(contentType == "multipart/form-data")
                    {
                        var multiPartContent = new MultipartFormDataContent();
                        var keyValueContent = data as IEnumerable<KeyValuePair<string, string>> ?? throw new ArgumentNullException(nameof(data), "Invalid Data Paramenters");
                        foreach(var kvp in keyValueContent)
                        {
                            multiPartContent.Add(new StringContent(kvp.Value), kvp.Key);
                        }
                        return multiPartContent;
                    }
                }
            }
            throw new ArgumentException("Unsupported Content Type");
        }

        private static async Task HandleHttpResponse(HttpResponseMessage response)
        {
            if(!response.IsSuccessStatusCode)
            {
                var errorMessage =  await response.Content.ReadAsStringAsync();
                throw new HttpRequestExceptionEx(response.StatusCode, errorMessage);
            }
        }

        private async Task HandleException(Exception ex)
        {
            //Show Alertbox with error message

            var message = ex is HttpRequestExceptionEx httpEx? httpEx.Message: ex.Message;

            if (string.IsNullOrWhiteSpace(message))
                message = "An unexpected error occurred.";

            await App.Current.MainPage.DisplayAlert(AppResources.Error, message, "OK");
        }

    }
}
