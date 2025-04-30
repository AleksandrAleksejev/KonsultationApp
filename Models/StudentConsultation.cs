using SQLite;

namespace KonsultationApp;

public class StudentConsultation
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int ConsultationId { get; set; }
    public DateTime RegistrationDateTime { get; set; }
}