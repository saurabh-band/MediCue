using MediCue.Locator;
using Microsoft.Maui.Controls.Xaml;
using MediCue.Services.Localization;

namespace MediCue.Extensions
{
    [ContentProperty(nameof(Key))]
    public class LocalizationExtension : Microsoft.Maui.Controls.Xaml.IMarkupExtension
    {
        public string Key { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (String.IsNullOrEmpty(Key))
                return String.Empty;

            var localizationService = ServiceLocator.GetService<ILocalizationService>();

            return localizationService?.GetString(Key) ?? Key;

        }
    }
}
