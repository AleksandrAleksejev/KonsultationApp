using Microsoft.Extensions.Logging;
using KonsultationApp.Models;
using KonsultationApp.Services;
using SQLite;

namespace KonsultationApp
{
    public static class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {
            
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<TeacherViewModel>();
            builder.Services.AddTransient<StudentViewModel>();
#endif

            return builder.Build();
        }
    }
}
