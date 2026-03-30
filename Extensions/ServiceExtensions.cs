using MediCue.Services.Navigation;
using MediCue.Services.UserActivity;
using MediCue.Services.UserPermissions;
using MediCue.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MediCue.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterAppServices(this IServiceCollection services)
        {
            // Register app services here
            services.AddSingleton<INavigationService, MauiNavigationService>();
            services.AddSingleton<IConnectivityService, ConnectivityService>();
            //services.AddSingleton<IRequestProvider, RequestProvider>();
            services.AddSingleton<IUserPermissionsService, UserPermissionsService>();
            services.AddSingleton(AppInfo.Current);
            services.AddSingleton<IInActivityService, InActivityService>();
        }

        public static void RegisterMappers(this IServiceCollection services)
        {
            // Register mappers here
            services.AddSingleton<IWebMapper<RecentCommunicationResponse, RecentCommunicationDTO>, RecentCommunicationWebMapper>();
            services.AddSingleton<IWebMapper<List<ChatBotRequestResponse>, List<ChatBotRequestDTO>>, ChatBotRequestWebMapper>();
            services.AddSingleton<IWebMapper<ChatBotOutputResponse, ChatBotOutputDTO>, ChatBotOutputWebMapper>();
        }

        public static void RegisterOtherServices(this IServiceCollection services)
        {
            // Register other services here
            services.AddSingleton<IChatBotServices, ChatBotService>();
        }

        public static void RegisterViewModels(this IServiceCollection services)
        {
            // Register view models here
            services.AddTransient<LoginViewModel>();
            //services.AddTransient<SignUpViewModel>();
            //services.AddTransient<ForgotPasswordViewModel>();
            //services.AddTransient<ResetPasswordViewModel>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<ReportSummaryTextViewModel>();
            services.AddTransient<MedicineSummaryViewModel>();
            services.AddTransient<MedicineRoutineSummaryViewModel>();
            services.AddTransient<ReportsAndSummaryViewModel>();
            services.AddTransient<MedicineScheduleViewModel>();
            services.AddTransient<UpdateMedicineScheduleViewModel>();
            services.AddTransient<ChatBotViewModel>();
            //services.AddTransient<ProfileViewModel>();
            services.AddTransient<RefillMedicineViewModel>();
            //services.AddTransient<AboutViewModel>();
        }

        public static void RegisterViews(this IServiceCollection services)
        {
            // Register views here
            services.AddTransient<LoginPage>();
            //services.AddTransient<SignUpPage>();
            //services.AddTransient<ForgotPasswordPage>();
            //services.AddTransient<ResetPasswordPage>();
            services.AddTransient<HomePage>();
            services.AddTransient<EmergencyContactPage>();
            services.AddTransient<PersonalDetailsPage>();
            services.AddTransient<MedicineSummaryPage>();
            services.AddTransient<ReportsAndSummaryPage>();
            services.AddTransient<ReportSummaryTextPage>();
            services.AddTransient<MedicineRoutineSummary>();
            services.AddTransient<UploadReportPage>();
            services.AddTransient<MedicineSchedulePage>();
            services.AddTransient<AddNewMedicineSchedulePage>();
            services.AddTransient<UpdateMedicineSchedulePage>();
            services.AddTransient<ChatBotPage>();
            services.AddTransient<ScanQRToRefillMedicine>();
            //services.AddTransient<ProfilePage>();
            services.AddTransient<AppSettingsPage>();
            services.AddTransient<RefillMedicinePage>();
        }

        public static void RegisterPopups(this IServiceCollection services)
        {
            // Register popups here
        }
    }
}