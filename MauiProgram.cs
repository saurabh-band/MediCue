namespace MediCue
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseLocalNotification()
                .UseBarcodeReader()
                .ConfigureSyncfusionToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureMauiHandlers(handlers =>
                {

                })
                .ConfigureLifecycleEvents(events =>
                {
                    void HandleAppResumed() => ServiceLocator.GetService<IInActivityService>()?.HandleAppResumedAsync();
                    void HandleAppMinimized() => ServiceLocator.GetService<IInActivityService>()?.HandleAppMinimizedAsync();
#if ANDROID
                    events.AddAndroid(android => android
                            .OnResume(activity => HandleAppResumed())
                            .OnPause(activity => HandleAppMinimized()));
#elif IOS
                    events.AddiOS(iOS => iOS
                    .OnActivated(app => HandleAppResumed())
                    .OnResignActivation(app => HandleAppMinimized()));
#endif
                });

#if DEV || DEBUG
            string enviornment = "Dev";
#elif QA || RELEASE
            string enviornment = "QA";
#elif PROD || PRODUCTION
            string enviornment = "Prod";
#endif
            // Register Services
            var configManager = LoadCongurationForEnviornment(enviornment);
            builder.Services.AddSingleton<IConfiguration>(configManager);

            var baseURLapi = configManager.GetValue<string>($"{General.APISETTINGS}:{General.BASEURL}") ?? string.Empty;

            builder.Services.AddHttpClient<IRequestProvider, RequestProvider>(client =>
            {
                client.BaseAddress = new Uri(baseURLapi);
                client.Timeout = TimeSpan.FromSeconds(General.DEFAULT_TIMEOUT_SECONDS);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var apiKey = configManager.GetValue<string>($"{General.APIKEYEURL}:{General.APIKEY}");
                if (!string.IsNullOrWhiteSpace(apiKey))
                {
                    client.DefaultRequestHeaders.Add("api-key", apiKey);
                }
            });

            //builder.Services.AddTransient<IRequestProvider>(sp =>
            //{
            //    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            //    var httpClient = httpClientFactory.CreateClient("RestHttpClient");
            //    var connectivityService = sp.GetRequiredService<IConnectivityService>();
            //    return new RequestProvider(httpClient, connectivityService);
            //});

            //Register Localization Services
            builder.Services.AddSingleton<ILocalizationService>(Provider =>
            new LocalizationService(new ResourceManager("MediCue.Resources.Strings.AppResources", typeof(MauiProgram).Assembly)));

            // App Services
            builder.Services.RegisterAppServices();

            //Other Services
            builder.Services.RegisterOtherServices();

            //Mappers
            builder.Services.RegisterMappers();

            //ViewModels
            builder.Services.RegisterViewModels();

            //Views / Pages
            builder.Services.RegisterViews();

            //Popups
            builder.Services.RegisterPopups();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var mauiApp = builder.Build();

            //Initialize Service Locator with service provider
            ServiceLocator.Initialize(mauiApp.Services);

            //Global Unhandled Exception Handling
            HandleGlobalException();

            return mauiApp;
        }

        private static IConfiguration LoadCongurationForEnviornment(string enviornment)
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string baseResourceName = $"{General.APP_NAMESPACE}.{General.APPSETTINGSJSON}";
                string resourceName = $"{baseResourceName}.{enviornment}.json";

                //Check for the base configuraton first
                string baseResource = $"{baseResourceName}.json";
                using Stream? baseStream = assembly.GetManifestResourceStream(baseResource);

                if(baseStream == null)
                {
                    throw new FileNotFoundException("Base configuration file not found in embedded resources.", baseResource);
                }

                var configBuilder = new ConfigurationBuilder().AddJsonStream(baseStream);

                //Add enviornment specific configuration
                using Stream? envStream = assembly.GetManifestResourceStream(resourceName);

                if (envStream == null)
                {
                    throw new FileNotFoundException("Environment configuration file not found in embedded resources.", resourceName);
                }

                configBuilder.AddJsonStream(envStream);

                return configBuilder.Build();
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading configuration", ex);
            }
        }

        private static void HandleGlobalException()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Exception ex = (Exception)e.ExceptionObject;
                System.Diagnostics.Debug.WriteLine($"Unhandled Exception: {ex.Message}\nStackTrace: {ex.StackTrace}");
            };

            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"Unobserved Task Exception: {e.Exception.Message}\nStackTrace: {e.Exception.StackTrace}");
                e.SetObserved();
            };
        }

    }
}
