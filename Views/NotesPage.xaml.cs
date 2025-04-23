using KonsultationApp.Models;

namespace KonsultationApp.Views;

public partial class NotesPage : ContentPage
{
    public NotesPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadNotes();
    }

    private async void OnCreateNoteClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NoteEntryPage));
    }

    private void LoadNotes()
    {
        var notes = new List<Note>();

        foreach (var file in Directory.GetFiles(FileSystem.AppDataDirectory, "*.txt"))
        {
            string text = File.ReadAllText(file);
            notes.Add(new Note { Text = text });
        }

        notesCollectionView.ItemsSource = notes.OrderByDescending(n => n.Created).ToList();
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Optional: добавить поведение при выборе элемента
    }
}
