namespace DTOs;

public class Client : BaseDTO
{
    public DateTime PayDay { get; set; }
    public DateTime LastPaidDate { get; set; }
    public string Status { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public int MembershipID { get; set; }
}