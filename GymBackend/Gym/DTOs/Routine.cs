﻿namespace DTOs;

public class Routine : BaseDTO
{
    public string Name { get; set; }
    public int ClientId { get; set; }
    public DateTime Created { get; set; }
}