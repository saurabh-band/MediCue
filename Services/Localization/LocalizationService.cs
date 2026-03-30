using System.Globalization;

namespace MediCue.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ResourceManager _resourceManager;

        public LocalizationService(ResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public string GetString(string key)
        {
            return _resourceManager.GetString(key, CultureInfo.CurrentCulture) ?? String.Empty;
        }

        public void SetCulture(CultureInfo culture)
        {
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
    }
}
