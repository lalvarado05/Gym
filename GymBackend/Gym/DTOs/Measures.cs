namespace DTOs;

public class Measures : BaseDTO
{
    public int ClientID { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public double AverageOfFat { get; set; }
    public DateTime Created { get; set; }
}