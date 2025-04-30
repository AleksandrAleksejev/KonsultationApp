using System.Collections.ObjectModel;
using System.Windows.Input;

public class StudentViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;

    // Изменили private поле на public свойство
    public int CurrentStudentId { get; set; }

    public ObservableCollection<Consultation> AvailableConsultations { get; }
    public ObservableCollection<StudentConsultation> MyRegistrations { get; }

    public ICommand LoadConsultationsCommand { get; }
    public ICommand RegisterForConsultationCommand { get; }
    public ICommand CancelRegistrationCommand { get; }
    public ICommand RefreshCommand { get; }

    public StudentViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        AvailableConsultations = new ObservableCollection<Consultation>();
        MyRegistrations = new ObservableCollection<StudentConsultation>();

        LoadConsultationsCommand = new Command(async () => await LoadData());
        RegisterForConsultationCommand = new Command<int>(async (id) => await RegisterForConsultation(id));
        CancelRegistrationCommand = new Command<int>(async (id) => await CancelRegistration(id));
        RefreshCommand = new Command(async () => await LoadData());

        LoadConsultationsCommand.Execute(null);
    }

    private async Task LoadData()
    {
        IsBusy = true;

        try
        {
            var consultations = await _databaseService.GetConsultations();
            AvailableConsultations.Clear();
            foreach (var consultation in consultations)
            {
                AvailableConsultations.Add(consultation);
            }

            // Заменяем _currentStudentId на CurrentStudentId
            if (CurrentStudentId > 0)
            {
                var registrations = await _databaseService.GetStudentRegistrations(CurrentStudentId);
                MyRegistrations.Clear();
                foreach (var reg in registrations)
                {
                    MyRegistrations.Add(reg);
                }
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task RegisterForConsultation(int consultationId)
    {
        // Заменяем _currentStudentId на CurrentStudentId
        if (CurrentStudentId == 0) return;

        var registration = new StudentConsultation
        {
            // Заменяем _currentStudentId на CurrentStudentId
            StudentId = CurrentStudentId,
            ConsultationId = consultationId,
            RegistrationDateTime = DateTime.Now
        };

        await _databaseService.RegisterForConsultation(registration);
        await LoadData();
    }

    private async Task CancelRegistration(int registrationId)
    {
        await _databaseService.DeleteStudentConsultation(registrationId);
        await LoadData();
    }
}