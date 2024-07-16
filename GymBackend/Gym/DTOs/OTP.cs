namespace DTOs;

public class OTP : BaseDTO
{
    public int OTPData { get; set; }
    public int UserID { get; set; }
    public DateTime ExpiredDate { get; set; }
    public string WasUsed { get; set; }
    public DateTime Created { get; set; }
}