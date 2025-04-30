using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KonsultationApp.ViewModels;

public class TeacherViewModel : BaseViewModel
{
    private readonly DatabaseService _databaseService;
    private int _currentTeacherId;

    public ObservableCollection<Consultation> Consultations { get; }
    public ObservableCollection<StudentConsultation> Registrations { get; }

    public ICommand LoadConsultationsCommand { get; }
    public ICommand AddConsultationCommand { get; }
    public ICommand DeleteConsultationCommand { get; }
    public ICommand ViewRegistrationsCommand { get; }
    public ICommand RefreshCommand { get; }

    public TeacherViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;
        Consultations = new ObservableCollection<Consultation>();
        Registrations = new ObservableCollection<StudentConsultation>();

        LoadConsultationsCommand = new Command(async () => await LoadConsultations());
        AddConsultationCommand = new Command(async () => await OnAddConsultation());
        DeleteConsultationCommand = new Command<int>(async (id) => await OnDeleteConsultation(id));
        ViewRegistrationsCommand = new Command<int>(async (id) => await OnViewRegistrations(id));
        RefreshCommand = new Command(async () => await LoadConsultations());

        LoadConsultationsCommand.Execute(null);
    }

    public void SetTeacherId(int teacherId)
    {
        _currentTeacherId = teacherId;
    }

    private async Task LoadConsultations()
    {
        IsBusy = true;
        try
        {
            var consultations = await _databaseService.GetConsultations();

            Consultations.Clear();
            foreach (var consultation in consultations)
            {
                if (consultation.TeacherId == _currentTeacherId)
                {
                    Consultations.Add(consultation);
                }
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task OnAddConsultation()
    {
        var newConsultation = new Consultation
        {
            Name = "Новая консультация",
            Date = DateTime.Today.AddDays(1),
            Time = new TimeSpan(15, 0, 0),
            Classroom = "Кабинет 101",
            TeacherId = _currentTeacherId
        };

        await _databaseService.AddConsultation(newConsultation);
        await LoadConsultations();
    }

    private async Task OnDeleteConsultation(int id)
    {
        await _databaseService.DeleteConsultation(id);
        await LoadConsultations();
    }

    private async Task OnViewRegistrations(int consultationId)
    {
        var registrations = await _databaseService.GetRegistrationsForConsultation(consultationId);

        Registrations.Clear();
        foreach (var registration in registrations)
        {
            Registrations.Add((StudentConsultation)registration);
        }
    }
}