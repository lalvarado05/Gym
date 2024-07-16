namespace DTOs;

public class AppliedDiscount : BaseDTO
{
    public int DiscountID { get; set; }
    public int ClientID { get; set; }
    public DateTime AppliedDate { get; set; }
    public DateTime Created { get; set; }
}