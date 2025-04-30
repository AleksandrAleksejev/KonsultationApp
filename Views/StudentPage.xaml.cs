namespace KonsultationApp;
using KonsultationApp.ViewModels;

public partial class StudentPage : ContentPage
{
    public StudentPage(StudentViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
