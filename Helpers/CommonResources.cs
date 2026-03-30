namespace MediCue.Helpers
{
    public class CommonResources
    {
        public static Color GetResourceColor(string key)
        {
            if(Application.Current != null && Application.Current.Resources.TryGetValue(key, out var color))
                return (Color)color;

            return Colors.White;
        }
    }
}
