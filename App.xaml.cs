using KonsultationApp;
using KonsultationApp.Models;
using KonsultationApp.Services;
using SQLite;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }
}