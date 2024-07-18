using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class UserManager
{
    public void Create(Schedule schedule)
    {
    }

    public void Update(Schedule schedule)
    {
    }

    public void Delete(Schedule schedule)
    {
    }

    public List<Schedule> RetrieveAll()
    {
        return null;
    }

    public User RetrieveById(int id)
    {
        var uCrud = new UserCrudFactory();
        return uCrud.RetrieveById<User>(id);
    }

    // Aquí irían las validaciones

    #region Validations

    #endregion
}