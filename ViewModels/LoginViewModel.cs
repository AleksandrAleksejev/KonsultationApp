namespace KonsultationApp.ViewModels;

public class LoginViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    private string _username;
    private string _password;

    public string Username
    {
        get => _username;
        set => SetProperty(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public Command LoginCommand { get; }  // Изменили на Command вместо ICommand
    public Command RegisterCommand { get; }

    public LoginViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;

        LoginCommand = new Command(async () => await OnLoginClicked(), CanLogin);
        RegisterCommand = new Command(async () => await OnRegisterClicked());

        // Подписываемся на изменение свойств
        PropertyChanged += (_, __) => ((Command)LoginCommand).ChangeCanExecute();
    }

    private bool CanLogin() => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);

    private async Task OnLoginClicked()
    {
        IsBusy = true;
        try
        {
            var user = await _databaseService.GetUser(Username, Password);

            if (user == null)
            {
                await Shell.Current.DisplayAlert("Ошибка", "Неверные учетные данные", "OK");
                return;
            }

            if (user.Role == "opetaja")
            {
                var teacherVM = new TeacherViewModel(_databaseService);
                teacherVM.SetTeacherId(user.Id);
                await Shell.Current.GoToAsync("//TeacherPage");
            }
            else if (user.Role == "opilane")
            {
                var studentVM = new StudentViewModel(_databaseService);
                studentVM.CurrentStudentId = user.Id;
                await Shell.Current.GoToAsync("//StudentPage");
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task OnRegisterClicked()
    {
        await Shell.Current.GoToAsync("//RegisterPage");
    }
}