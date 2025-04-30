using KonsultationApp.ViewModels;

namespace KonsultationApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Регистрация маршрутов для навигации
        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(StudentPage), typeof(StudentPage));
        Routing.RegisterRoute(nameof(TeacherPage), typeof(TeacherPage));
    }
}