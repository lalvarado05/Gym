namespace DTOs;

public class Meetings : BaseDTO
{
    public int ClientUserID { get; set; }
    public int EmployeeID { get; set; }
    public TimeOnly TimeOfEntry { get; set; }
    public TimeOnly TimeOfExit { get; set; }
    public DateTime ProgramedDate { get; set; }
    public string IsCancelled { get; set; }
    public DateTime Created { get; set; }
}