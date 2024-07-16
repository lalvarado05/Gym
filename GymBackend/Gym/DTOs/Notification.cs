namespace DTOs;

public class Notification : BaseDTO
{
    public int UserID { get; set; }
    public string NotificationContent { get; set; }
    public int WasRead { get; set; }
    public DateTime Created { get; set; }
}