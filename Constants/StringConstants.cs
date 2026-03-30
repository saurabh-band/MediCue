namespace MediCue.Constants
{
    public static class StringConstants
    {
        public static class General
        {
            public const string APISETTINGS = "API";
            public const string BASEURL = "BaseUrl";
            public const string APIKEYEURL = "BaseUrl";
            public const int DEFAULT_TIMEOUT_SECONDS = 60;

            public const string APIKEY = "0f356654d12f43ec9bb0362768b6a3b6" ;

            public const string APP_NAMESPACE = "MediCue";
            public const string APPSETTINGSJSON = "appsettings";
        }


        public static class Errors
        {
            public const string NETWORK_ERROR = "Network connection lost. Please check your internet settings.";
            public const string AUTHENTICATION_FAILED = "Authentication failed. Please check your credentials.";
            public const string UNKNOWN_ERROR = "An unknown error has occurred.";
        }

        public static class RequestProviderKeys
        {
            public const string APPLICATION_JSON = "application/json";
        }

        public static class ShellPageRoutes
        {
            public const string LOGIN_PAGE = "//Login";
            public const string HOME_PAGE = "//Home";
            public const string MAIN_PAGE = "//Main";
        }
    }
}
