using KonsultationApp.ViewModels;

namespace KonsultationApp.ViewModels;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
