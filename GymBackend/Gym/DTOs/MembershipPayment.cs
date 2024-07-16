namespace DTOs;

public class MembershipPayment : BaseDTO
{
    public int ClientID { get; set; }
    public int MembershipID { get; set; }
    public DateTime PaymentDate { get; set; }
    public double Amount { get; set; }
    public double AmountAfterDiscount { get; set; }
    public int AppliedDiscountID { get; set; }
    public string PaymentMethod { get; set; }
    public string IsConfirmed { get; set; }
    public DateTime Created { get; set; }
}