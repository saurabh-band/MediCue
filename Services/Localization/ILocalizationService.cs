using System.Globalization;

namespace MediCue.Services.Localization
{
    public interface ILocalizationService
    {
        string GetString(string key);
        void SetCulture(CultureInfo culture);
    }
}
