using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class PersonalTrainingManager
{
    public void Create(PersonalTraining personalTraining)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        // Validaciones adicionales
        ptCrud.Create(personalTraining);
    }

    public void Update(PersonalTraining personalTraining)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        ptCrud.Update(personalTraining);
    }

    public void Delete(PersonalTraining personalTraining)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        ptCrud.Delete(personalTraining);
    }

    public List<PersonalTraining> RetrieveAll()
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        return ptCrud.RetrieveAll<PersonalTraining>();
    }

    public PersonalTraining RetrieveById(int id)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        return ptCrud.RetrieveById<PersonalTraining>(id);
    }

    // Aquí irían las validaciones

    #region Validations

    #endregion
}