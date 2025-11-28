using System.Net;
using System.Text.Json;

namespace ISM.WebApp.Utilities;

public static class PersianErrorTranslator
{
    private class ValidationProblem
    {
        public Dictionary<string, string[]> Errors { get; set; } = new();
        public string? Title { get; set; }
        public string? Detail { get; set; }
    }

    public static async Task<string> ToFriendlyMessage(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return "دسترسی مجاز نیست. لطفاً مجدداً وارد شوید.";
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            return "شما مجوز انجام این عملیات را ندارید.";
        }

        try
        {
            var validation = await response.Content.ReadFromJsonAsync<ValidationProblem>();
            if (validation?.Errors?.Any() == true)
            {
                var messages = validation.Errors.SelectMany(kvp => kvp.Value.Select(v => $"{kvp.Key}: {v}"));
                return string.Join(" - ", messages);
            }

            if (!string.IsNullOrWhiteSpace(validation?.Detail))
            {
                return validation.Detail;
            }

            if (!string.IsNullOrWhiteSpace(validation?.Title))
            {
                return validation.Title;
            }
        }
        catch (JsonException)
        {
            // ignored - fallback below
        }

        return "خطایی در پردازش درخواست رخ داد. لطفاً دوباره تلاش کنید.";
    }
}
