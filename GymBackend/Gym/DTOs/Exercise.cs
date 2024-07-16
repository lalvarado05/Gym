namespace DTOs;

public class Exercise : BaseDTO
{
    public int EquipmentID { get; set; }
    public int TypeID { get; set; }
    public string Name { get; set; }
    public int Sets { get; set; }
    public int Weight { get; set; }
    public int Reps { get; set; }
    public int Duration { get; set; }
}