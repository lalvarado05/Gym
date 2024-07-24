using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class UserGroupClassManager
{
    public void Create(UserGroupClass userGroupClass)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        // Validaciones adicionales
        ugcCrud.Create(userGroupClass);
    }

    public void Update(UserGroupClass userGroupClass)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        ugcCrud.Update(userGroupClass);
    }

    public void Delete(UserGroupClass userGroupClass)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        ugcCrud.Delete(userGroupClass);
    }

    public List<UserGroupClass> RetrieveAll()
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        return ugcCrud.RetrieveAll<UserGroupClass>();
    }

    public UserGroupClass RetrieveById(int id)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        return ugcCrud.RetrieveById<UserGroupClass>(id);
    }

    // Aquí irían las validaciones

    #region Validations

    #endregion
}