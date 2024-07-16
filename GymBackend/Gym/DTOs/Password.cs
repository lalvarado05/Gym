namespace DTOs;

public class Password : BaseDTO
{
    public int UserID { get; set; }
    public string PasswordContent { get; set; }
    public DateTime Created { get; set; }
}