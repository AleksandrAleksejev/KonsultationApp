namespace KonsultationApp.Views;
using KonsultationApp.Models;
using KonsultationApp.Services;
using SQLite;

public partial class NoteEntryPage : ContentPage
{
    public NoteEntryPage()
    {
        InitializeComponent();
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        string text = noteEditor.Text;
        string filename = Path.Combine(FileSystem.AppDataDirectory, $"notes.txt");
        File.WriteAllText(filename, text);
        await Shell.Current.GoToAsync("..");
    }
}
