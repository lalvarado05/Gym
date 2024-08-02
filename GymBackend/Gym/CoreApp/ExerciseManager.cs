using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class ExerciseManager
{
    public void Create(Exercise exercise)
    {
        var eCrud = new ExerciseCrudFactory();
        ExerciseTypeValidation(exercise);
        eCrud.Create(exercise);
    }

    public void Update(Exercise exercise)
    {
        var eCrud = new ExerciseCrudFactory();
        ExerciseTypeValidation(exercise);
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

    #region Validations

    public void ExerciseTypeValidation(Exercise exercise)
    {
        if(exercise.Type == "Peso")
        {
            if(exercise.Weight < 1)
            {
                throw new Exception("El peso es requerido para ejercicios basados en peso");
            }

            exercise.Duration = 0;
        }

        if (exercise.Type == "Tiempo")
        {
            if (exercise.Duration < 1)
            {
                throw new Exception("El tiempo es requerido para ejercicios basados en tiempo");
            }

            exercise.Sets = 0;
            exercise.Reps = 0;
            exercise.Weight = 0;
        }

        if (exercise.Type == "AMRAP")
        {
            if (exercise.Reps < 1)
            {
                throw new Exception("Las repeticiones son requeridas para ejercicios basados en AMRP");
            }

            exercise.Sets = 0;
            exercise.Duration = 0;
        }

        if (string.IsNullOrWhiteSpace(exercise.Name) ||
            string.IsNullOrWhiteSpace(exercise.Type) ||
            exercise.EquipmentId <= 0 ||
            string.IsNullOrWhiteSpace(exercise.EquipmentName) ||
            exercise.Sets < 0 ||
            exercise.Reps < 0 ||
            exercise.Weight < 0 ||
            exercise.Duration < 0)
        {
            throw new Exception("Faltan campos requeridos");
        }
    }

    #endregion
}