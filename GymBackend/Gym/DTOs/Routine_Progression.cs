namespace DTOs;

public class Routine_Progression : BaseDTO
{
    public int RoutineID { get; set; }
    public int ExerciseID { get; set; }
    public int Sets { get; set; }
    public int Weigth { get; set; }
    public int Reps { get; set; }
    public int Duration { get; set; }
    public string Comments { get; set; }
    public DateTime Created { get; set; }
}