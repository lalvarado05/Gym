namespace DTOs;

public class DiscountCoupon : BaseDTO
{
    public int DiscountID { get; set; }
    public string CouponCode { get; set; }
    public DateTime Created { get; set; }
}