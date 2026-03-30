namespace MediCue.Locator
{
    public static class ServiceLocator
    {
        private static IServiceProvider? _serviceProvider;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static T GetService<T>() where T : class
        {
            return (T)(_serviceProvider?.GetService(typeof(T)) ?? throw new Exception("Could Not locate the service"));
        }
    }
}
