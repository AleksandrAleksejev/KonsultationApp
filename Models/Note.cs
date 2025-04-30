using KonsultationApp.Models;
using KonsultationApp.Services;
using SQLite;

public class Note
{
    public string Text { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}
