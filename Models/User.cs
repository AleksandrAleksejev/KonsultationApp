using SQLite;

namespace KonsultationApp;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } // "opetaja" или "opilane"
}