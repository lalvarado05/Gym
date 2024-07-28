﻿using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class ExerciseManager
{
    public void Create(Exercise exercise)
    {
        var eCrud = new ExerciseCrudFactory();
        eCrud.Create(exercise);
    }

    public void Update(Exercise exercise)
    {
        var eCrud = new ExerciseCrudFactory();
        eCrud.Update(exercise);
    }

    public void Delete(Exercise exercise)
    {
        var eCrud = new ExerciseCrudFactory();
        eCrud.Delete(exercise);
    }

    public List<Exercise> RetrieveAll()
    {
        var eCrud = new ExerciseCrudFactory();
        return eCrud.RetrieveAll<Exercise>();
    }
}