using KonsultationApp.Models;

namespace KonsultationApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var note = (Note)e.CurrentSelection.FirstOrDefault();
        if (note != null)
        {
            await Navigation.PushAsync(new NotesPage());


        }
    }

    private async void OnAddNoteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotesPage());
    }

}
