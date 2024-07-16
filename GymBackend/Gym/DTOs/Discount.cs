namespace DTOs;

public class Discount : BaseDTO
{
    public int TypeID { get; set; }
    public int Percentage { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public DateTime Created { get; set; }
}