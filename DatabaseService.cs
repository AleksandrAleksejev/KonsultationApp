
using SQLite;


namespace KonsultationApp;

public class DatabaseService
{
    private SQLiteAsyncConnection _database;

    public DatabaseService()
    {
        _database = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "consultations.db"));
        InitializeDatabase();
    }

    private async void InitializeDatabase()
    {
        await _database.CreateTableAsync<User>();
        await _database.CreateTableAsync<Consultation>();
        await _database.CreateTableAsync<StudentConsultation>();

        // Добавляем тестовых пользователей, если их нет
        if (await _database.Table<User>().CountAsync() == 0)
        {
            await _database.InsertAsync(new User { Name = "teacher1", Password = "pass1", Role = "opetaja" });
            await _database.InsertAsync(new User { Name = "student1", Password = "pass1", Role = "opilane" });
        }
    }

    // Методы для работы с пользователями
    public async Task<User> GetUser(string name, string password)
    {
        return await _database.Table<User>()
            .Where(u => u.Name == name && u.Password == password)
            .FirstOrDefaultAsync();
    }

    public async Task<int> AddUser(User user)
    {
        return await _database.InsertAsync(user);
    }

    // Методы для работы с консультациями
    public async Task<List<Consultation>> GetConsultations()
    {
        return await _database.Table<Consultation>().ToListAsync();
    }

    public async Task<int> AddConsultation(Consultation consultation)
    {
        return await _database.InsertAsync(consultation);
    }

    public async Task<int> UpdateConsultation(Consultation consultation)
    {
        return await _database.UpdateAsync(consultation);
    }

    public async Task<int> DeleteConsultation(int id)
    {
        return await _database.DeleteAsync<Consultation>(id);
    }

    // Методы для регистрации на консультации
    public async Task<int> RegisterForConsultation(StudentConsultation registration)
    {
        return await _database.InsertAsync(registration);
    }

    public async Task<List<StudentConsultation>> GetStudentRegistrations(int studentId)
    {
        return await _database.Table<StudentConsultation>()
            .Where(sc => sc.StudentId == studentId)
            .ToListAsync();
    }

    internal async Task DeleteStudentConsultation(int registrationId)
    {
        throw new NotImplementedException();
    }

    internal async Task<IEnumerable<object>> GetRegistrationsForConsultation(int consultationId)
    {
        throw new NotImplementedException();
    }
}