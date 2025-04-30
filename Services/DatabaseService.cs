using KonsultationApp.Models; 
using KonsultationApp.Services; 
using SQLite;

namespace KonsultationApp.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            if (_database == null)
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, "consultations.db3");
                _database = new SQLiteAsyncConnection(dbPath);

                // Создаем таблицы
                await _database.CreateTableAsync<User>();
                await _database.CreateTableAsync<Consultation>();
                await _database.CreateTableAsync<StudentConsultation>();

                // Тестовые данные (если нужно)
                if (await _database.Table<User>().CountAsync() == 0)
                {
                    await _database.InsertAsync(new User { Name = "teacher1", Password = "123", Role = "opetaja" });
                    await _database.InsertAsync(new User { Name = "student1", Password = "123", Role = "opilane" });
                }
            }
        }

        // Методы для пользователей
        public async Task<User> GetUser(string name, string password)
        {
            return await _database.Table<User>()
                .Where(u => u.Name == name && u.Password == password)
                .FirstOrDefaultAsync();
        }

        // Методы для консультаций
        public async Task<List<Consultation>> GetConsultations()
        {
            return await _database.Table<Consultation>().ToListAsync();
        }

        public async Task<int> AddConsultation(Consultation consultation)
        {
            return await _database.InsertAsync(consultation);
        }

        // Методы для регистраций студентов
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

        public async Task<int> DeleteStudentConsultation(int id)
        {
            return await _database.DeleteAsync<StudentConsultation>(id);
        }
        public async Task<int> DeleteConsultation(int id)
        {
            // Сначала удаляем все регистрации на эту консультацию
            await _database.Table<StudentConsultation>()
                .Where(sc => sc.ConsultationId == id)
                .DeleteAsync();

            // Затем удаляем саму консультацию
            return await _database.DeleteAsync<Consultation>(id);
        }

        public async Task<List<StudentConsultation>> GetRegistrationsForConsultation(int consultationId)
        {
            return await _database.Table<StudentConsultation>()
                .Where(sc => sc.ConsultationId == consultationId)
                .ToListAsync();
        }
    }
}