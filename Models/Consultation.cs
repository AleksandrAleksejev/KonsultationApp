using KonsultationApp.Models; 
using KonsultationApp.Services; 
using SQLite;


public class Consultation
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string Classroom { get; set; }
    public int TeacherId { get; set; }
}