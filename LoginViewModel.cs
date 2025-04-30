using KonsultationApp.Models;
using KonsultationApp.Services;
using SQLite;
public class LoginViewModel : BaseViewModel
{
    private DatabaseService _databaseService;

    public string Username { get; set; }
    public string Password { get; set; }

    public Command LoginCommand { get; }
    public Command RegisterCommand { get; }

    public LoginViewModel()
    {
        _databaseService = new DatabaseService();
        LoginCommand = new Command(OnLoginClicked);
        RegisterCommand = new Command(OnRegisterClicked);
    }

    // В LoginViewModel.cs
    private async void OnLoginClicked()
    {
        var user = await _databaseService.GetUser(Username, Password);

        if (user != null)
        {
            if (user.Role == "opetaja")
            {
                await Shell.Current.GoToAsync("//TeacherPage");
            }
            else if (user.Role == "opilane")
            {
                // Передаем ID ученика в StudentViewModel
                var studentVM = new StudentViewModel(_databaseService);
                studentVM.CurrentStudentId = user.Id;
                await Shell.Current.GoToAsync("//StudentPage");
            }
        }
    }

    private async void OnRegisterClicked()
    {
        await Shell.Current.GoToAsync("//RegisterPage");
    }
}