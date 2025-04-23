using KonsultationApp.Views;

namespace KonsultationApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(NoteEntryPage), typeof(NoteEntryPage));
    }
}
