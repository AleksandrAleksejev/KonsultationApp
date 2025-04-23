namespace KonsultationApp.Views;

public partial class NoteEntryPage : ContentPage
{
    public NoteEntryPage()
    {
        InitializeComponent();
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        string text = noteEditor.Text;
        string filename = Path.Combine(FileSystem.AppDataDirectory, $"{Path.GetRandomFileName()}.txt");
        File.WriteAllText(filename, text);
        await Shell.Current.GoToAsync("..");
    }
}
