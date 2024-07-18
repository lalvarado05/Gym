namespace DTOs;

public class User : BaseDTO
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Phone { get; set; }

    public string Email { get; set; }

    public DateTime LastLogin { get; set; }

    public string Status { get; set; }

    public string Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime Created { get; set; }
}