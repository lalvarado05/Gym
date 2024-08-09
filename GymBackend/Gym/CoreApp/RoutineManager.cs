using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class RoutineManager
    {
        public void Create(Routine routine, List<int> exerciseIds)
        {
            var rCrud = new RoutineCrudFactory();
            var reCrud = new RoutineExerciseCrudFactory();

            // Crear la rutina
            rCrud.Create(routine);

            // Recuperar el último RoutineId generado
            var lastRoutine = rCrud.RetrieveLastRoutine();

            // Registrar los ejercicios asociados
            foreach (var exerciseId in exerciseIds)
            {
                var routineExercise = new Routine_Exercise
                {
                    RoutineId = lastRoutine.Id,
                    ExerciseId = exerciseId
                };
                reCrud.Create(routineExercise);
            }
        }
    }
}
