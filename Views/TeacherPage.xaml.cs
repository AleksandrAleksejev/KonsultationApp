namespace KonsultationApp;
using KonsultationApp.ViewModels;

public partial class TeacherPage : ContentPage
{
    public TeacherPage(TeacherViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
