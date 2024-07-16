namespace DTOs;

public class PersonalTraining : BaseDTO
{
    public int ClientUserID { get; set; }
    public int EmployeeID { get; set; }
    public string IsCancelled { get; set; }
    public string IsPaid { get; set; }
    public TimeOnly TimeOfEntry { get; set; }
    public TimeOnly TimeOfExit { get; set; }
    public DateTime ProgrammedDate { get; set; }
}