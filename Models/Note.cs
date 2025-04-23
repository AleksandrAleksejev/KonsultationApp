namespace KonsultationApp.Models;

public class Note
{
    public string Text { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
}
