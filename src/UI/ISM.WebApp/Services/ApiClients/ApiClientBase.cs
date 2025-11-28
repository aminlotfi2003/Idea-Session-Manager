using ISM.WebApp.Utilities;

namespace ISM.WebApp.Services.ApiClients;

public abstract class ApiClientBase
{
    protected ApiClientBase(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    protected HttpClient HttpClient { get; }

    protected async Task<T?> ReadAsAsync<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>();
        }

        var message = await PersianErrorTranslator.ToFriendlyMessage(response);
        throw new InvalidOperationException(message);
    }

    protected async Task EnsureSuccess(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var message = await PersianErrorTranslator.ToFriendlyMessage(response);
        throw new InvalidOperationException(message);
    }
}
